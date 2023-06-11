namespace Checkout.PaymentGateway.Model.Entities;

public class CardEntity
{
    public Guid Id { get; set; }
    public int ClusterKey { get; set; }
    public string Number { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string Name { get; set; }
    public string CVV { get; set; }

    public PaymentEntity Payment { get; set; }
}