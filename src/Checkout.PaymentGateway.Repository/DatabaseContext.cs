using Checkout.PaymentGateway.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.Repository;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<PaymentEntity> Payments { get; set; }
    public DbSet<CardEntity> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        PaymentEntity.OnModelCreating(modelBuilder.Entity<PaymentEntity>());
        CardEntity.OnModelCreating(modelBuilder.Entity<CardEntity>());
    }
}