using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using microStore.Services.InventoryApi.Models;



namespace microStore.Services.InventoryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 1,
                Quantity = 20,
                RetailPrice = 100000,
                VendorId = 1,
                ProductId = 20
            });
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 2,
                Quantity = 453,
                RetailPrice = 100000,
                VendorId = 2,
                ProductId = 21
            });
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 3,
                Quantity = 20,
                RetailPrice = 100000,
                VendorId = 3,
                ProductId = 22
            });
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 4,
                Quantity = 45,
                RetailPrice = 100000,
                VendorId = 4,
                ProductId = 23
            });
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 5,
                Quantity = 5,
                RetailPrice = 100000,
                VendorId = 5,
                ProductId = 24
            });
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 6,
                Quantity = 3,
                RetailPrice = 100000,
                VendorId = 6,
                ProductId = 25
            });
            modelBuilder.Entity<Inventory>().HasData(new Inventory
            {
                InventoryId = 7,
                Quantity = 84,
                RetailPrice = 100000,
                VendorId = 7,
                ProductId = 26
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 1,
                VendorName = "Vendor1"
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 2,
                VendorName = "Vendor1"
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 3,
                VendorName = "Vendor2"
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 4,
                VendorName = "Vendor1"
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 5,
                VendorName = "Vendor1"
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 6,
                VendorName = "Vendor1"
            });
            modelBuilder.Entity<Vendor>().HasData(new Vendor
            {
                VendorId = 7,
                VendorName = "Vendor1"
            });

        }
    }
}
