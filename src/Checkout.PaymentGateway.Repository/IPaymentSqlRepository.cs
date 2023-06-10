using Checkout.PaymentGateway.Repository.Entities;

namespace Checkout.PaymentGateway.Repository;
public interface IPaymentSqlRepository
{
    Task<PaymentEntity> GetPaymentByIdAsync(Guid id);
    Task CreatePaymentAsync(PaymentEntity entity);
}
