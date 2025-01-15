


using Microsoft.EntityFrameworkCore;
using microStore.Services.OrderApi.Models;

namespace microStore.Services.OrderApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderHeader>()
                .HasMany(o => o.OrderDetails)
                .WithOne()
                .HasForeignKey(oi => oi.OrderHeaderId);

            modelBuilder.Entity<OrderHeader>()
                .HasOne(o => o.Payment)
                .WithOne()
                .HasForeignKey<OrderHeader>(o => o.PaymentId);

            modelBuilder.Entity<OrderHeader>()
                .HasOne(o => o.Shipping)
                .WithOne()
                .HasForeignKey<OrderHeader>(o => o.ShippingId);

            base.OnModelCreating(modelBuilder);

            // Seed de órdenes
            modelBuilder.Entity<OrderHeader>().HasData(
                new OrderHeader
                {
                    OrderHeaderId = 1,
                    UserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                    CouponCode = "20% off",
                    Status = OrderStatus.Completed,
                    Discount = 20,
                    ShippingId = 1,
                    PaymentId = 1,
                    Name = "user1",
                    Phone = "513255565",
                    Email = "ccc@cc.com",
                    OrderTime = DateTime.UtcNow,
                    OrderTotal = 3000m - 3000m * (20m / 100m)
                },
                new OrderHeader
                {
                    OrderHeaderId = 2,
                    UserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                    CouponCode = "20% off",
                    Status = OrderStatus.Completed,
                    Discount = 20,
                    ShippingId = 2,
                    PaymentId = 2,
                    Name = "user1",
                    Phone = "513255565",
                    Email = "ccc@cc.com",
                    OrderTime = DateTime.UtcNow,
                    OrderTotal = 3000m - 3000m * (20m / 100m)
                }
            );

            // Seed de OrderItems
            modelBuilder.Entity<OrderDetails>().HasData(
                new OrderDetails
                {
                    OrderDetailsId = 1,
                    OrderHeaderId = 1,
                    ProductId = 3,
                    ProductName = "Producto A",
                    UnitPrice = 25.00m,
                    Quantity = 2
                },
                new OrderDetails
                {
                    OrderDetailsId = 2,
                    OrderHeaderId = 1,
                    ProductId = 2,
                    ProductName = "Producto B",
                    UnitPrice = 25.00m,
                    Quantity = 1
                },
                new OrderDetails
                {
                    OrderDetailsId = 3,
                    OrderHeaderId = 2,
                    ProductId = 3,
                    ProductName = "Producto A",
                    UnitPrice = 25.00m,
                    Quantity = 2
                },
                new OrderDetails
                {
                    OrderDetailsId = 4,
                    OrderHeaderId = 2,
                    ProductId = 2,
                    ProductName = "Producto B",
                    UnitPrice = 25.00m,
                    Quantity = 1
                }
            );

            // Seed de Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentId = 1,
                    Method = PaymentMethod.CreditCard,
                    Amount = 75.00m,
                    PaidAt = DateTime.UtcNow.AddDays(-2),
                    Status = PaymentStatus.Paid
                },
                new Payment
                {
                    PaymentId = 2,
                    Method = PaymentMethod.CreditCard,
                    Amount = 75.00m,
                    PaidAt = DateTime.UtcNow.AddDays(-2),
                    Status = PaymentStatus.Paid
                }
            );

            // Seed de Shippings
            modelBuilder.Entity<Shipping>().HasData(
                new Shipping
                {
                    ShippingId = 1,
                    Address = "Calle 123, Bogotá",
                    City = "Bogotá",
                    PostalCode = "110111",
                    Country = "Colombia",
                    Status = ShippingStatus.Pending,
                    ShippedAt = DateTime.UtcNow.AddDays(-2),
                    DeliveredAt = DateTime.UtcNow.AddDays(-2)
                },
                new Shipping
                {
                    ShippingId = 2,
                    Address = "Calle 123, Bogotá",
                    City = "Bogotá",
                    PostalCode = "110111",
                    Country = "Colombia",
                    Status = ShippingStatus.Pending,
                    ShippedAt = DateTime.UtcNow.AddDays(-2),
                    DeliveredAt = DateTime.UtcNow.AddDays(-2)
                }
            );

        }
    }
}
