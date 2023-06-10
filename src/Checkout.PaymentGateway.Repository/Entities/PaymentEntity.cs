using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGateway.Repository.Entities;

public class PaymentEntity
{
    public Guid Id { get; set; }
    public int ClusterKey { get; set; }
    public decimal Amount { get; set; }
    public Guid CardId { get; set; }
    public required string CurrencyCode { get; set; }
    public required CardEntity Card { get; set; }

    internal static void OnModelCreating(EntityTypeBuilder<PaymentEntity> entityTypeBuilder)
    {
        entityTypeBuilder
            .ToTable("Payment");

        entityTypeBuilder
            .HasIndex(e => e.ClusterKey)
            .IsClustered(true);

        entityTypeBuilder
            .Property(e => e.ClusterKey)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        entityTypeBuilder
            .HasKey(e => e.Id)
            .IsClustered(false);

        entityTypeBuilder
            .Property(e => e.Id)
            .ValueGeneratedNever();

        entityTypeBuilder
            .Property(e => e.Amount)
            .HasColumnType("money")
            .IsRequired();

        entityTypeBuilder
            .Property(e => e.CurrencyCode)
            .HasMaxLength(3)
            .IsRequired();

        entityTypeBuilder
            .HasOne(e => e.Card)
            .WithOne(c => c.Payment)
            .HasForeignKey<PaymentEntity>(c => c.CardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}