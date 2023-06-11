namespace Checkout.PaymentGateway.Model.Entities;

public class PaymentEntity
{
    public Guid Id { get; set; }
    public int ClusterKey { get; set; }
    public decimal Amount { get; set; }
    public Guid CardId { get; set; }
    public string CurrencyCode { get; set; }
    public string Status { get; set; }
    public CardEntity Card { get; set; }
}