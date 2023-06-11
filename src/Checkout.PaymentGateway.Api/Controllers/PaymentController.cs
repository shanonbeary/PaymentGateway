using Checkout.PaymentGateway.Model;
using Checkout.PaymentGateway.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.Api;

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

            return Ok(paymentResponseDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet("payments/{id:Guid}")]
    public async Task<IActionResult> GetPaymentByIdAsync(Guid id)
    {
        var paymentDetails = await _paymentService.GetPaymentByIdAsync(id);

        return Ok(paymentDetails);
    }
}
