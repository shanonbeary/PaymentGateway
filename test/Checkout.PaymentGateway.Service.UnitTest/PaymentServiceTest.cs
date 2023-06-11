using Checkout.AcquiringBank.Client;
using Checkout.PaymentGateway.Model.Dto;
using Checkout.PaymentGateway.Repository;
using Checkout.PaymentGateway.Model.Entities;
using Checkout.PaymentGateway.Service.Utilities;
using Moq;
using Checkout.PaymentGateway.Model.Exceptions;

namespace Checkout.PaymentGateway.Service.UnitTest;

public class PaymentServiceTest
{
    [Fact]
    public async Task CreatePaymentAsync_Success()
    {
        // Arrange
        var paymentRequestDto = new PaymentRequestDto
        {
            CurrencyCode = "NZD",
            Amount = 99.99m,
            Card = new PaymentRequestDto.CardRequestDto
            {
                Name = "Joe Blogs",
                Number = "1111-1111-1111-1111",
                ExpiryMonth = 10,
                ExpiryYear = 23,
                CVV = "123"
            }
        };

        var acquiringBankPaymentResponse = new AcquiringBankPaymentResponseDto
        {
            Id = Guid.NewGuid(),
            CurrencyCode = paymentRequestDto.CurrencyCode,
            Amount = paymentRequestDto.Amount,
            Status = "Accepted"
        };

        var paymentRepository = new Mock<IPaymentRepository>();
        var acquiringBankClient = new Mock<IAcquiringBankClient>();
        acquiringBankClient.Setup(x => x.RequestPaymentAsync(It.IsAny<AcquiringBankPaymentRequestDto>())).ReturnsAsync(acquiringBankPaymentResponse);
        var paymentService = new PaymentService(paymentRepository.Object, acquiringBankClient.Object);

        // Act
        var response = await paymentService.CreatePaymentAsync(paymentRequestDto);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Accepted", response.Status);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_Success()
    {
        // Arrange
        var paymentEntity = new PaymentEntity
        {
            Id = Guid.NewGuid(),
            Amount = 10.99m,
            CurrencyCode = "EUR",
            Card = new CardEntity
            {
                Id = Guid.NewGuid(),
                Number = "1111-1111-1111-1111",
                ExpiryMonth = 10,
                ExpiryYear = 23,
                Name = "Joe Blog",
                CVV = "123"
            }
        };
        var maskedCardNumber = CardMaskUtility.MaskCardNumber(paymentEntity.Card.Number);

        var paymentRepository = new Mock<IPaymentRepository>();
        paymentRepository.Setup(x => x.GetPaymentByIdAsync(paymentEntity.Id)).ReturnsAsync(paymentEntity);
        var acquiringBankClient = new Mock<IAcquiringBankClient>();

        var paymentService = new PaymentService(paymentRepository.Object, acquiringBankClient.Object);

        // Act
        var response = await paymentService.GetPaymentByIdAsync(paymentEntity.Id);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(paymentEntity.Id, response.Id);
        Assert.Equal(paymentEntity.Amount, response.Amount);
        Assert.Equal(paymentEntity.CurrencyCode, response.CurrencyCode);
        Assert.Equal(maskedCardNumber, response.Card.MaskedNumber);
        Assert.Equal(paymentEntity.Card.ExpiryMonth, response.Card.ExpiryMonth);
        Assert.Equal(paymentEntity.Card.ExpiryYear, response.Card.ExpiryYear);
        Assert.Equal(paymentEntity.Card.Name, response.Card.Name);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_EntityNotFound()
    {
        // Arrange
        var paymentId = Guid.NewGuid();

        var paymentRepository = new Mock<IPaymentRepository>();
        paymentRepository.Setup(x => x.GetPaymentByIdAsync(paymentId)).ReturnsAsync((PaymentEntity)null);
        var acquiringBankClient = new Mock<IAcquiringBankClient>();

        var paymentService = new PaymentService(paymentRepository.Object, acquiringBankClient.Object);

        // Act Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => paymentService.GetPaymentByIdAsync(paymentId));
    }
}