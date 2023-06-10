namespace Checkout.BankSimulator.Stub.Api;

public class PaymentApiRequestModel
{
    public string CurrencyCode { get; set; }
    public decimal Amount { get; set; }
    public CardApiRequestModel Card { get; set; }

    public class CardApiRequestModel
    {
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string Name { get; set; }
    }
}