using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using microStore.Services.ProductApi.Models;
using System.Reflection.Metadata;


namespace microStore.Services.ProductApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductsImages> ProductsImages { get; set; }
        public DbSet<ProductsCategories> ProductsCategories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<ProductPropertyValue> ProductPropertyValues { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .HasMany(e => e.Categories)
                        .WithMany(e => e.Products)
                        .UsingEntity<ProductsCategories>(
                        l => l.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId),
                        r => r.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId)
                ).HasKey(pi => new { pi.ProductId, pi.CategoryId });
            modelBuilder.Entity<Product>()
                       .HasMany(e => e.Images)
                       .WithMany(e => e.Products)
                       .UsingEntity<ProductsImages>(
                       l => l.HasOne<ProductImages>().WithMany().HasForeignKey(e => e.ImageId),
                       r => r.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId)
               ).HasKey(pi => new { pi.ProductId, pi.ImageId });
            modelBuilder.Entity<Product>()
                        .HasMany(e => e.Properties)
                        .WithMany(e => e.Products)
                        .UsingEntity<ProductPropertyValue>()
                        .HasKey(ppv => new { ppv.ProductId, ppv.PropertyValueId });

            modelBuilder.Entity<PropertyType>()
                .HasMany(e => e.Properties)
                .WithOne()
                .HasForeignKey(e => e.PropertyTypeId)
                .IsRequired();
            modelBuilder.Entity<Property>()
                 .HasMany(p => p.PropertyValues)
                 .WithOne(pv => pv.Property)
                 .HasForeignKey(pv => pv.PropertyId)
                 .IsRequired();

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Samosa",
                Link = "samosa-1",
                Current_price = 3000000,
                Previous_price = 3500000,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                BrandId = 1,
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Link = "paneer-tikka-2",
                Current_price = 3000000,
                Previous_price = 3500000,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                BrandId = 2,
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Link = "sweet-pie-3",
                Current_price = 3000000,
                Previous_price = 3500000,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                BrandId = 3,
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Link = "pav-bhaji-4",
                Current_price = 3000000,
                Previous_price = 3500000,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                BrandId = 4,
            });
            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 1,
                BrandName = "Brand 1"
            });
            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 2,
                BrandName = "Brand 2"
            });
            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 3,
                BrandName = "Brand 3"
            });
            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 4,
                BrandName = "Brand 4"
            });
            modelBuilder.Entity<Brand>().HasData(new Brand
            {
                BrandId = 5,
                BrandName = "Brand 5"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 1,
                CategoryName = "Cat 1",
                CategoryLevel = 0,
                CategoryParentId = 0
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 2,
                CategoryName = "Cat 2",
                CategoryLevel = 1,
                CategoryParentId = 1
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 3,
                CategoryName = "Cat 3",
                CategoryLevel = 2,
                CategoryParentId = 2
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 4,
                CategoryName = "Cat 4",
                CategoryLevel = 1,
                CategoryParentId = 1
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 5,
                CategoryName = "Cat 5",
                CategoryLevel = 2,
                CategoryParentId = 4
            });

            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 1,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 7,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 2,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 3,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 4,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 5,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });
            modelBuilder.Entity<ProductImages>().HasData(new ProductImages
            {
                ImageId = 6,
                ImageLabel = "image1",
                ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA",

            });


            modelBuilder.Entity<ProductsImages>().HasData(
      new ProductsImages { ImageId = 1, ProductId = 2 },
      new ProductsImages { ImageId = 2, ProductId = 2 },
      new ProductsImages { ImageId = 3, ProductId = 1 },
      new ProductsImages { ImageId = 4, ProductId = 3 },
      new ProductsImages { ImageId = 5, ProductId = 3 },
      new ProductsImages { ImageId = 6, ProductId = 4 },
      new ProductsImages { ImageId = 7, ProductId = 4 }
  );
            modelBuilder.Entity<ProductsCategories>().HasData(
    new ProductsCategories { CategoryId = 1, ProductId = 2 },
    new ProductsCategories { CategoryId = 2, ProductId = 2 },
    new ProductsCategories { CategoryId = 3, ProductId = 2 },
    new ProductsCategories { CategoryId = 1, ProductId = 3 },
    new ProductsCategories { CategoryId = 2, ProductId = 3 },
    new ProductsCategories { CategoryId = 3, ProductId = 3 },
     new ProductsCategories { CategoryId = 1, ProductId = 1 },
    new ProductsCategories { CategoryId = 2, ProductId = 1 },
    new ProductsCategories { CategoryId = 3, ProductId = 1 },
     new ProductsCategories { CategoryId = 1, ProductId = 4 },
    new ProductsCategories { CategoryId = 2, ProductId = 4 },
    new ProductsCategories { CategoryId = 3, ProductId = 4 }


);
            //////////////////////////////////////

            modelBuilder.Entity<PropertyType>().HasData(new PropertyType
            {
                PropertyTypeId = 1,
                PropertyTypeName = "Caracteristicas Principales",
                PropertyTypeDescription = "Caracteristicas mas destacables de los productos",
                PropertyTypeOrder = 1,
            });
            modelBuilder.Entity<PropertyType>().HasData(new PropertyType
            {
                PropertyTypeId = 2,
                PropertyTypeName = "Funciones",
                PropertyTypeDescription = "Funciones principales de los productos",
                PropertyTypeOrder = 2,
            });
            modelBuilder.Entity<PropertyType>().HasData(new PropertyType
            {
                PropertyTypeId = 3,
                PropertyTypeName = "Componentes",
                PropertyTypeDescription = "Componentes que componen el producto",
                PropertyTypeOrder = 3,
            });
            ////////////////////////////////////


            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 1,
                PropertyName = "Memoria Interna",
                PropertyTypeId = 1,
                PropertyOrder = 1,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 2,
                PropertyName = "Memoria RAM",
                PropertyTypeId = 1,
                PropertyOrder = 2,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 3,
                PropertyName = "Modelo del Procesador",
                PropertyTypeId = 3,
                PropertyOrder = 2,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 4,
                PropertyName = "Frecuencia",
                PropertyTypeId = 3,
                PropertyOrder = 2,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 5,
                PropertyName = "Tipo de pantalla",
                PropertyTypeId = 1,
                PropertyOrder = 3,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 6,
                PropertyName = "Resolucion",
                PropertyTypeId = 1,
                PropertyOrder = 4,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 7,
                PropertyName = "Tamaño bateria",
                PropertyTypeId = 3,
                PropertyOrder = 3,
            });
            modelBuilder.Entity<Property>().HasData(new Property
            {
                PropertyId = 8,
                PropertyName = "Bateria",
                PropertyTypeId = 3,
                PropertyOrder = 4,
            });

            /////////////////////////////////////

            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 1,
                PropertyValueName = "512GB",
                PropertyId = 1,
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 2,
                PropertyId = 1,
                PropertyValueName = "256GB",

            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 3,
                PropertyId = 1,
                PropertyValueName = "128GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 4,
                PropertyId = 1,
                PropertyValueName = "64GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 5,
                PropertyId = 1,
                PropertyValueName = "1000GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 6,
                PropertyId = 2,
                PropertyValueName = "8GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 7,
                PropertyId = 2,
                PropertyValueName = "4GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 8,
                PropertyId = 2,
                PropertyValueName = "6GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 9,
                PropertyId = 2,
                PropertyValueName = "12GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 10,
                PropertyId = 2,
                PropertyValueName = "16GB",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 11,
                PropertyId = 3,
                PropertyValueName = "Mediatek",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 12,
                PropertyId = 3,
                PropertyValueName = "Qualcomm",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 13,
                PropertyId = 4,
                PropertyValueName = "3.3 GHz",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 14,
                PropertyId = 4,
                PropertyValueName = "2.8 GHz",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 15,
                PropertyId = 5,
                PropertyValueName = "AMOLED",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 16,
                PropertyId = 5,
                PropertyValueName = "LED",
            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 18,
                PropertyId = 6,
                PropertyValueName = "1080 x 2400 px • FHD+",

            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 19,
                PropertyId = 6,
                PropertyValueName = "1440 x 3168 px • WQHD+",

            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 20,
                PropertyId = 7,
                PropertyValueName = "6.55\"",

            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 21,
                PropertyId = 7,
                PropertyValueName = "6.82\"",

            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 22,
                PropertyId = 8,
                PropertyValueName = "5000 mAh",

            });
            modelBuilder.Entity<PropertyValue>().HasData(new PropertyValue
            {
                PropertyValueId = 23,
                PropertyId = 8,
                PropertyValueName = "4000 mAh",
            });

        }
    }
}
