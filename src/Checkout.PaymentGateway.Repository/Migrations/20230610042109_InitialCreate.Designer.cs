﻿// <auto-generated />
using System;
using Checkout.PaymentGateway.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Checkout.PaymentGateway.Repository.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230610042109_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Checkout.PaymentGateway.Repository.Entities.CardEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterKey"));

                    b.Property<int>("Cvv")
                        .HasColumnType("int");

                    b.Property<string>("EncryptedCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaskedCardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ClusterKey");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterKey"));

                    b.ToTable("Card", (string)null);
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Repository.Entities.PaymentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClusterKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClusterKey"));

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.HasIndex("ClusterKey");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("ClusterKey"));

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Repository.Entities.PaymentEntity", b =>
                {
                    b.HasOne("Checkout.PaymentGateway.Repository.Entities.CardEntity", "Card")
                        .WithOne("Payment")
                        .HasForeignKey("Checkout.PaymentGateway.Repository.Entities.PaymentEntity", "CardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Checkout.PaymentGateway.Repository.Entities.CardEntity", b =>
                {
                    b.Navigation("Payment");
                });
#pragma warning restore 612, 618
        }
    }
}