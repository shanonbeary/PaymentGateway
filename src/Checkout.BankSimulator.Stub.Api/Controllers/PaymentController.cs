using Microsoft.AspNetCore.Mvc;
using Checkout.BankSimulator.Stub.Api.Dto;

namespace Checkout.BankSimulator.Stub.Api.Controllers;

[ApiController]
public class PaymentController : ControllerBase
{
    private readonly List<string> _validCreditCards = new()
    {
        "1111-1111-1111-1111",
    };

    [HttpPost("payments")]
    public IActionResult CreatePayment([FromBody] PaymentRequestDto requestModel)
    {
        if (_validCreditCards.Contains(requestModel.Card.Number))
        {
            return new ObjectResult(new PaymentResponseDto
            {
                Id = Guid.NewGuid(),
                Amount = requestModel.Amount,
                CurrencyCode = requestModel.CurrencyCode,
                Status = "Accepted"
            })
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
        else
        {
            return new ObjectResult(new PaymentErrorResponseDto
            {
                ErrorType = "InvalidCard",
                ErrorMessage = "Your card number is invalid"
            })
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };
        }
    }
}
