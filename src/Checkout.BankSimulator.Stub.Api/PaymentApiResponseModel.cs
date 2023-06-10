namespace Checkout.BankSimulator.Stub.Api;
public class PaymentApiResponseModel
{
    public Guid Id { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}
