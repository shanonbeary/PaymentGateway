using Checkout.PaymentGateway.Model.Dto;
using Checkout.PaymentGateway.Service;
using Microsoft.AspNetCore.Mvc;
using Checkout.AcquiringBank.Client;
using System.Net;
using Checkout.PaymentGateway.Model.Exceptions;

namespace Checkout.PaymentGateway.Api.Controllers;

[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("payments")]
    public async Task<IActionResult> RequestPaymentAsync([FromBody] PaymentRequestDto paymentRequest)
    {
        try
        {
            var paymentResponseDto = await _paymentService.CreatePaymentAsync(paymentRequest);

            return new ObjectResult(paymentResponseDto)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
        catch (UnprocessableEntityException ex)
        {
            return new ObjectResult(new PaymentErrorResponseDto
            {
                ErrorCode = ex.ErrorResponse.ErrorType,
                ErrorMessage = ex.ErrorResponse.ErrorMessage
            })
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };
        }
        catch (UnexpectedStatusCodeException ex)
        {
            return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    [HttpGet("payments/{id:Guid}")]
    public async Task<IActionResult> GetPaymentByIdAsync(Guid id)
    {
        try
        {
            var paymentDetails = await _paymentService.GetPaymentByIdAsync(id);

            return Ok(paymentDetails);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
