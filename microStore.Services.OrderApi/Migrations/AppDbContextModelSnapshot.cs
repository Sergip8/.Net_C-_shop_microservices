﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using microStore.Services.OrderApi.Data;

#nullable disable

namespace microStore.Services.OrderApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("microStore.Services.OrderApi.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailsId"));

                    b.Property<int>("OrderHeaderId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderHeaderId1")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("OrderHeaderId");

                    b.HasIndex("OrderHeaderId1");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            OrderDetailsId = 1,
                            OrderHeaderId = 1,
                            ProductId = 3,
                            ProductName = "Producto A",
                            Quantity = 2,
                            UnitPrice = 25.00m
                        },
                        new
                        {
                            OrderDetailsId = 2,
                            OrderHeaderId = 1,
                            ProductId = 2,
                            ProductName = "Producto B",
                            Quantity = 1,
                            UnitPrice = 25.00m
                        },
                        new
                        {
                            OrderDetailsId = 3,
                            OrderHeaderId = 2,
                            ProductId = 3,
                            ProductName = "Producto A",
                            Quantity = 2,
                            UnitPrice = 25.00m
                        },
                        new
                        {
                            OrderDetailsId = 4,
                            OrderHeaderId = 2,
                            ProductId = 2,
                            ProductName = "Producto B",
                            Quantity = 1,
                            UnitPrice = 25.00m
                        });
                });

            modelBuilder.Entity("microStore.Services.OrderApi.Models.OrderHeader", b =>
                {
                    b.Property<int>("OrderHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderHeaderId"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderHeaderId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("ShippingId")
                        .IsUnique();

                    b.ToTable("OrderHeaders");

                    b.HasData(
                        new
                        {
                            OrderHeaderId = 1,
                            CouponCode = "20% off",
                            Discount = 20m,
                            Email = "ccc@cc.com",
                            Name = "user1",
                            OrderTime = new DateTime(2024, 12, 11, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4457),
                            OrderTotal = 2400.0m,
                            PaymentId = 1,
                            Phone = "513255565",
                            ShippingId = 1,
                            Status = 3,
                            UserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15"
                        },
                        new
                        {
                            OrderHeaderId = 2,
                            CouponCode = "20% off",
                            Discount = 20m,
                            Email = "ccc@cc.com",
                            Name = "user1",
                            OrderTime = new DateTime(2024, 12, 11, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4461),
                            OrderTotal = 2400.0m,
                            PaymentId = 2,
                            Phone = "513255565",
                            ShippingId = 2,
                            Status = 3,
                            UserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15"
                        });
                });

            modelBuilder.Entity("microStore.Services.OrderApi.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaidAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            PaymentId = 1,
                            Amount = 75.00m,
                            Method = 0,
                            PaidAt = new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4584),
                            Status = 1
                        },
                        new
                        {
                            PaymentId = 2,
                            Amount = 75.00m,
                            Method = 0,
                            PaidAt = new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4590),
                            Status = 1
                        });
                });

            modelBuilder.Entity("microStore.Services.OrderApi.Models.Shipping", b =>
                {
                    b.Property<int>("ShippingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShippingId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeliveredAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ShippedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ShippingId");

                    b.ToTable("Shippings");

                    b.HasData(
                        new
                        {
                            ShippingId = 1,
                            Address = "Calle 123, Bogotá",
                            City = "Bogotá",
                            Country = "Colombia",
                            DeliveredAt = new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4613),
                            PostalCode = "110111",
                            ShippedAt = new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4610),
                            Status = 0
                        },
                        new
                        {
                            ShippingId = 2,
                            Address = "Calle 123, Bogotá",
                            City = "Bogotá",
                            Country = "Colombia",
                            DeliveredAt = new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4616),
                            PostalCode = "110111",
                            ShippedAt = new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4615),
                            Status = 0
                        });
                });

            modelBuilder.Entity("microStore.Services.OrderApi.Models.OrderDetails", b =>
                {
                    b.HasOne("microStore.Services.OrderApi.Models.OrderHeader", null)
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("microStore.Services.OrderApi.Models.OrderHeader", "OrderHeader")
                        .WithMany()
                        .HasForeignKey("OrderHeaderId1");

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("microStore.Services.OrderApi.Models.OrderHeader", b =>
                {
                    b.HasOne("microStore.Services.OrderApi.Models.Payment", "Payment")
                        .WithOne()
                        .HasForeignKey("microStore.Services.OrderApi.Models.OrderHeader", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("microStore.Services.OrderApi.Models.Shipping", "Shipping")
                        .WithOne()
                        .HasForeignKey("microStore.Services.OrderApi.Models.OrderHeader", "ShippingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("microStore.Services.OrderApi.Models.OrderHeader", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
