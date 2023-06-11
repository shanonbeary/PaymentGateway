using Checkout.PaymentGateway.Model.Entities;
using Checkout.PaymentGateway.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.Repository;

public class DatabaseContext : DbContext
{
    public DatabaseContext() { }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public virtual DbSet<PaymentEntity> Payments { get; set; }
    public virtual DbSet<CardEntity> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CardEntityTypeConfiguration());
    }
}