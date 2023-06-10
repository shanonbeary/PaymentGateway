namespace Checkout.PaymentGateway.Model;

public class PaymentRequestDto
{
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public CardRequestDto Card { get; set; }
    public class CardRequestDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
    }
}
