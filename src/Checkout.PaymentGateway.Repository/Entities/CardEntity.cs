using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkout.PaymentGateway.Repository.Entities;

public class CardEntity
{
    public Guid Id { get; set; }
    public int ClusterKey { get; set; }
    public required string EncryptedCardNumber { get; set; }
    public required string MaskedCardNumber { get; set; }
    public DateTime ExpiryDate { get; set; }
    public required string Name { get; set; }
    public int Cvv { get; set; }

    public required PaymentEntity Payment { get; set; }

    internal static void OnModelCreating(EntityTypeBuilder<CardEntity> entityTypeBuilder)
    {
        entityTypeBuilder
            .ToTable("Card");

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
    }
}