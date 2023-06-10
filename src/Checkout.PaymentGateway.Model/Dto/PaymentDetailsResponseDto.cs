namespace Checkout.PaymentGateway.Model;
public class PaymentDetailsResponseDto
{
    public Guid Id { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public CardDetailsResponseDto Card { get; set; }
    public class CardDetailsResponseDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
    }
}
