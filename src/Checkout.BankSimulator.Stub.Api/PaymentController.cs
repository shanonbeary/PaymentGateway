using Microsoft.AspNetCore.Mvc;

namespace Checkout.BankSimulator.Stub.Api;
public class PaymentController : ControllerBase
{
    private readonly List<string> _validCreditCards = new()
    {
        "1111-1111-1111-1111",
    };

    [HttpPost("payments")]
    public IActionResult CreatePayment([FromBody] PaymentApiRequestModel requestModel)
    {
        if (_validCreditCards.Contains(requestModel.Card.Number))
        {
            return Ok(new PaymentApiResponseModel
            {
                Id = Guid.NewGuid(),
                Amount = requestModel.Amount,
                CurrencyCode = requestModel.CurrencyCode,
                Status = "Accepted"
            });
        }
        else
        {
            return BadRequest(new PaymentApiErrorResponseModel
            {
                ErrorType = "InvalidCard",
                ErrorMessage = "Your card nunber is invalid"
            });
        }
    }
}
