using Checkout.PaymentGateway.Repository;
using Checkout.PaymentGateway.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Checkout.PaymentGateway.Respository.UnitTest;

public class PaymentSqlRepositoryTest
{
    [Fact]
    public async Task CreatePaymentAsync_Success()
    {
        // Arrange
        var databaseContextMock = new Mock<DatabaseContext>();
        var paymentSqlRepository = new PaymentSqlRepository(databaseContextMock.Object);
        var paymentEntity = new PaymentEntity
        {
            Id = Guid.NewGuid(),
            Amount = 10.99m,
            CurrencyCode = "EUR",
            Card = new CardEntity
            {
                Id = Guid.NewGuid(),
                Number = "1111-1111-1111-1111",
                ExpiryMonth = 10,
                ExpiryYear = 23,
                Name = "Joe Blog",
                CVV = "123"
            }
        };

        // Act
        await paymentSqlRepository.CreatePaymentAsync(paymentEntity);

        // Assert
        databaseContextMock.Verify(x => x.Add(It.Is<PaymentEntity>(p => p == paymentEntity)), Times.Once());
        databaseContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once());
    }

    [Fact]
    public async Task GetPaymentByIdAsync_Success()
    {
        // Arrange
        var testPaymentEntity =
            new PaymentEntity
            {
                Id = Guid.NewGuid(),
                Amount = 10.99m,
                CurrencyCode = "EUR",
                Card = new CardEntity
                {
                    Id = Guid.NewGuid(),
                    Number = "1111-1111-1111-1111",
                    ExpiryMonth = 10,
                    ExpiryYear = 23,
                    Name = "Joe Blog",
                    CVV = "123"
                }
            };

        var databaseContextMock = new Mock<DatabaseContext>();
        databaseContextMock.Setup(x => x.Payments).ReturnsDbSet(new List<PaymentEntity> { testPaymentEntity });

        var paymentSqlRepository = new PaymentSqlRepository(databaseContextMock.Object);

        // Act
        var paymentEntity = await paymentSqlRepository.GetPaymentByIdAsync(testPaymentEntity.Id);

        // Assert
        Assert.NotNull(paymentEntity);
        Assert.Equal(testPaymentEntity.Id, paymentEntity.Id);
        Assert.Equal(testPaymentEntity.Amount, paymentEntity.Amount);
        Assert.Equal(testPaymentEntity.CurrencyCode, paymentEntity.CurrencyCode);
        Assert.Equal(testPaymentEntity.Card.Id, paymentEntity.Card.Id);
        Assert.Equal(testPaymentEntity.Card.Number, paymentEntity.Card.Number);
        Assert.Equal(testPaymentEntity.Card.ExpiryMonth, paymentEntity.Card.ExpiryMonth);
        Assert.Equal(testPaymentEntity.Card.ExpiryYear, paymentEntity.Card.ExpiryYear);
        Assert.Equal(testPaymentEntity.Card.Name, paymentEntity.Card.Name);
        Assert.Equal(testPaymentEntity.Card.CVV, paymentEntity.Card.CVV);
    }
}