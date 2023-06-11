using System;
using Checkout.PaymentGateway.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checkout.PaymentGateway.Repository.Configuration;


public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
	public void Configure(EntityTypeBuilder<PaymentEntity> entityTypeBuilder)
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

