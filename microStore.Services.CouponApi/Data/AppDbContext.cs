using Microsoft.EntityFrameworkCore;
using microStore.Services.CouponApi.Models;

namespace microStore.Services.CouponApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "Puto1",
                ExpirationDate = DateTime.Now,
                ActivationCount = 1,
                DiscountAmount = 30,
                MinAmount = 10,
            }); modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "Puto2",
                ExpirationDate = DateTime.Now,
                ActivationCount = 1,
                DiscountAmount = 35,
                MinAmount = 20,
            }); modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                CouponCode = "Puto3",
                ExpirationDate = DateTime.Now,
                ActivationCount = 1,
                DiscountAmount = 40,
                MinAmount = 10,
            }); modelBuilder.Entity<Coupon>().HasData(new Coupon
            {

                CouponId = 4,
                CouponCode = "Puto4",
                ExpirationDate = DateTime.Now,
                ActivationCount = 1,
                DiscountAmount = 20,
                MinAmount = 80,
            }); modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 5,
                CouponCode = "Puto5",
                ExpirationDate = DateTime.Now,
                ActivationCount = 1,
                DiscountAmount = 10,
                MinAmount = 56,
            });
        }
    }
}
