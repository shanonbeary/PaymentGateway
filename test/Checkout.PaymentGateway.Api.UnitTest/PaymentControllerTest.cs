using Checkout.AcquiringBank.Client;
using Checkout.PaymentGateway.Api.Controllers;
using Checkout.PaymentGateway.Model.Dto;
using Checkout.PaymentGateway.Model.Exceptions;
using Checkout.PaymentGateway.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Checkout.PaymentGateway.Api.UnitTest;

public class PaymentControllerTest
{
    [Fact]
    public async Task RequestPaymentAsync_Success()
    {
        var paymentRequest = new PaymentRequestDto
        {
            CurrencyCode = "NZD",
            Amount = 22.22m,
            Card = new PaymentRequestDto.CardRequestDto
            {
                Number = "1111-1111-1111-1111",
                Name = "Joe Blog",
                ExpiryMonth = 10,
                ExpiryYear = 23,
                CVV = "123"
            }
        };

        var paymentResponse = new PaymentResponseDto
        {
            Id = Guid.NewGuid(),
            Status = "Accepted"
        };

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.CreatePaymentAsync(paymentRequest)).ReturnsAsync(paymentResponse);
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.RequestPaymentAsync(paymentRequest);

        var objectResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        Assert.IsType<PaymentResponseDto>(objectResult.Value);
    }

    [Fact]
    public async Task RequestPaymentAsync_UnprocessableEntityException()
    {
        var paymentRequest = new PaymentRequestDto
        {
            CurrencyCode = "NZD",
            Amount = 22.22m,
            Card = new PaymentRequestDto.CardRequestDto
            {
                Number = "0000-0000-0000-0000",
                Name = "Joe Blog",
                ExpiryMonth = 10,
                ExpiryYear = 23,
                CVV = "123"
            }
        };

        var acquiringBankPaymentErrorResponse = new AcquiringBankPaymentErrorResonseDto
        {
            ErrorType = "InvalidCard",
            ErrorMessage = "Card is invalid"
        };

        var unprocessableEntityException = new UnprocessableEntityException(acquiringBankPaymentErrorResponse);

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.CreatePaymentAsync(paymentRequest)).ThrowsAsync(unprocessableEntityException);
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.RequestPaymentAsync(paymentRequest);

        var objectResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal(StatusCodes.Status422UnprocessableEntity, objectResult.StatusCode);
        var errorResponse = Assert.IsType<PaymentErrorResponseDto>(objectResult.Value);
        Assert.Equal(errorResponse.ErrorCode, acquiringBankPaymentErrorResponse.ErrorType);
        Assert.Equal(errorResponse.ErrorMessage, errorResponse.ErrorMessage);
    }

    [Fact]
    public async Task RequestPaymentAsync_UnexpectedStatusCodeException()
    {
        var paymentRequest = new PaymentRequestDto
        {
            CurrencyCode = "NZD",
            Amount = 22.22m,
            Card = new PaymentRequestDto.CardRequestDto
            {
                Number = "0000-0000-0000-0000",
                Name = "Joe Blog",
                ExpiryMonth = 10,
                ExpiryYear = 23,
                CVV = "123"
            }
        };

        var unexpectedStatusCodeException = new UnexpectedStatusCodeException(System.Net.HttpStatusCode.InternalServerError, "Unhandled Exception");

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.CreatePaymentAsync(paymentRequest)).ThrowsAsync(unexpectedStatusCodeException);
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.RequestPaymentAsync(paymentRequest);

        var objectResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal(StatusCodes.Status502BadGateway, objectResult.StatusCode);
        var errorResponse = Assert.IsType<string>(objectResult.Value);
        Assert.Equal(errorResponse, unexpectedStatusCodeException.Message);
    }

    [Fact]
    public async Task RequestPaymentAsync_Exception()
    {
        var paymentRequest = new PaymentRequestDto();

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.CreatePaymentAsync(paymentRequest)).ThrowsAsync(new Exception());
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.RequestPaymentAsync(paymentRequest);

        var objectResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_Success()
    {
        var paymentDetailsResponse = new PaymentDetailsResponseDto
        {
            Id = Guid.NewGuid(),
            CurrencyCode = "NZD",
            Amount = 22.22m,
            Card = new PaymentDetailsResponseDto.CardDetailsResponseDto
            {
                MaskedNumber = "****-****-****-1234",
                Name = "Joe Blog",
                ExpiryMonth = 10,
                ExpiryYear = 23
            }
        };

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.GetPaymentByIdAsync(paymentDetailsResponse.Id)).ReturnsAsync(paymentDetailsResponse);
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.GetPaymentByIdAsync(paymentDetailsResponse.Id);

        var objectResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        var paymentDetailsControllerResponse = Assert.IsType<PaymentDetailsResponseDto>(objectResult.Value);
        Assert.Equal(paymentDetailsResponse.Id, paymentDetailsControllerResponse.Id);
        Assert.Equal(paymentDetailsResponse.CurrencyCode, paymentDetailsControllerResponse.CurrencyCode);
        Assert.Equal(paymentDetailsResponse.Amount, paymentDetailsControllerResponse.Amount);
        Assert.Equal(paymentDetailsResponse.Status, paymentDetailsControllerResponse.Status);
        Assert.Equal(paymentDetailsResponse.Card.MaskedNumber, paymentDetailsControllerResponse.Card.MaskedNumber);
        Assert.Equal(paymentDetailsResponse.Card.Name, paymentDetailsControllerResponse.Card.Name);
        Assert.Equal(paymentDetailsResponse.Card.ExpiryMonth, paymentDetailsControllerResponse.Card.ExpiryMonth);
        Assert.Equal(paymentDetailsResponse.Card.ExpiryYear, paymentDetailsControllerResponse.Card.ExpiryYear);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_EntityNotFoundException()
    {
        var paymentId = Guid.NewGuid();

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.GetPaymentByIdAsync(paymentId)).ThrowsAsync(new EntityNotFoundException());
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.GetPaymentByIdAsync(paymentId);

        var objectResult = Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_InternalServerError()
    {
        var paymentId = Guid.NewGuid();

        var paymentService = new Mock<IPaymentService>();
        paymentService.Setup(x => x.GetPaymentByIdAsync(paymentId)).ThrowsAsync(new Exception());
        var paymentController = new PaymentController(paymentService.Object);

        var response = await paymentController.GetPaymentByIdAsync(paymentId);

        var objectResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
    }
}
