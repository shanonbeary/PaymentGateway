using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Checkout.PaymentGateway.Model.Entities;

namespace Checkout.PaymentGateway.Repository;

public class CardEntityTypeConfiguration : IEntityTypeConfiguration<CardEntity>
{
    public void Configure(EntityTypeBuilder<CardEntity> entityTypeBuilder)
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
            .Property(e => e.Number)
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
