namespace Checkout.AcquiringBank.Client;

public class AcquiringBankPaymentResponseDto
{
    public Guid Id { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}