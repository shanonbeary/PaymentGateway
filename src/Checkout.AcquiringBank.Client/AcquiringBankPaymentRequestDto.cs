namespace Checkout.AcquiringBank.Client;
public class AcquiringBankPaymentRequestDto
{
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public AcquiringBankCardRequestDto Card { get; set; }

    public class AcquiringBankCardRequestDto
    {
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string Name { get; set; }
    }
}
