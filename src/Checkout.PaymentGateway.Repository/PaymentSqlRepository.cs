using Checkout.PaymentGateway.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.Repository;
public class PaymentSqlRepository : IPaymentRepository
{
    private readonly DatabaseContext _databaseContext;

    public PaymentSqlRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task CreatePaymentAsync(PaymentEntity entity)
    {
        _databaseContext.Add(entity);

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<PaymentEntity> GetPaymentByIdAsync(Guid id)
    {
        var query = (
            from p in _databaseContext.Payments
            where p.Id == id
            select p
        );

        return await query.FirstOrDefaultAsync();
    }
}
