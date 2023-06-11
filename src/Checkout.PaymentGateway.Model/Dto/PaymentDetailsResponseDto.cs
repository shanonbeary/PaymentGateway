namespace Checkout.PaymentGateway.Model.Dto;
public class PaymentDetailsResponseDto
{
    public Guid Id { get; set; }
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public CardDetailsResponseDto Card { get; set; }

    public class CardDetailsResponseDto
    {
        public string MaskedNumber { get; set; }
        public string Name { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}
