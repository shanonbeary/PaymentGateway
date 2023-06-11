using Checkout.PaymentGateway.Model.Entities;

namespace Checkout.PaymentGateway.Repository;

public interface IPaymentRepository
{
    Task<PaymentEntity> GetPaymentByIdAsync(Guid id);
    Task CreatePaymentAsync(PaymentEntity entity);
}
