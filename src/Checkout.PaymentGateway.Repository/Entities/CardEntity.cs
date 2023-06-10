using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkout.PaymentGateway.Repository.Entities;

public class CardEntity
{
    public Guid Id { get; set; }
    public int ClusterKey { get; set; }
    public string EncryptedNumber { get; set; }
    public string MaskedNumber { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string Name { get; set; }
    public string CVV { get; set; }

    public PaymentEntity Payment { get; set; }

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

        entityTypeBuilder
            .Property(e => e.EncryptedNumber)
            .IsRequired();

        entityTypeBuilder
            .Property(e => e.MaskedNumber)
            .IsRequired();

        entityTypeBuilder
            .Property(e => e.ExpiryMonth)
            .IsRequired();

        entityTypeBuilder
            .Property(e => e.ExpiryYear)
            .IsRequired();

        entityTypeBuilder
            .Property(e => e.Name)
            .IsRequired();

        entityTypeBuilder
            .Property(e => e.CVV)
            .IsRequired();
    }
}