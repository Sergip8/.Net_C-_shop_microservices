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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }

        //public DbSet<ProductsImages> ProductsImages { get; set; }
        //public DbSet<ProductsCategories> ProductsCategories { get; set; }
        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyValue> PropertyValues { get; set; }

        //public DbSet<ProductPropertyValue> ProductPropertyValues { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductCategory",
                    r => r.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                    l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                ).HasData(
                    new { CategoryId = 1, ProductId = 1 }, // Tecnología
                    new { CategoryId = 2, ProductId = 1 }, // TV, Audio y Video
                    new { CategoryId = 3, ProductId = 1 }, // Televisores

                    // Televisor LG OLED 65"
                    new { CategoryId = 1, ProductId = 2 }, // Tecnología
                    new { CategoryId = 2, ProductId = 2 }, // TV, Audio y Video
                    new { CategoryId = 3, ProductId = 2 }, // Televisores

                    // Soporte para TV hasta 55"
                    new { CategoryId = 1, ProductId = 3 }, // Tecnología
                    new { CategoryId = 2, ProductId = 3 }, // TV, Audio y Video
                    new { CategoryId = 4, ProductId = 3 }, // Accesorios TV

                    // Cable HDMI 4K 3 metros
                    new { CategoryId = 1, ProductId = 4 }, // Tecnología
                    new { CategoryId = 2, ProductId = 4 }, // TV, Audio y Video
                    new { CategoryId = 4, ProductId = 4 }, // Accesorios TV

                    // Portátil Lenovo ThinkPad
                    new { CategoryId = 1, ProductId = 5 }, // Tecnología
                    new { CategoryId = 2, ProductId = 5 }, // Computadores y Accesorios
                    new { CategoryId = 5, ProductId = 5 }, // Computadores Portátiles

                    // Portátil HP Pavilion
                    new { CategoryId = 1, ProductId = 6 }, // Tecnología
                    new { CategoryId = 2, ProductId = 6 }, // Computadores y Accesorios
                    new { CategoryId = 5, ProductId = 6 }, // Computadores Portátiles

                    // Bocina Bose SoundLink
                    new { CategoryId = 1, ProductId = 7 }, // Tecnología
                    new { CategoryId = 2, ProductId = 7 }, // Audio y Video
                    new { CategoryId = 6, ProductId = 7 }, // Parlantes

                    // Parlante JBL Flip 5
                    new { CategoryId = 1, ProductId = 8 }, // Tecnología
                    new { CategoryId = 2, ProductId = 8 }, // Audio y Video
                    new { CategoryId = 6, ProductId = 8 }, // Parlantes

                    // iPhone 14 Pro
                    new { CategoryId = 1, ProductId = 9 }, // Tecnología
                    new { CategoryId = 4, ProductId = 9 }, // Celulares
                    new { CategoryId = 7, ProductId = 9 }, // Smartphones

                    // Xiaomi Redmi Note 12
                    new { CategoryId = 1, ProductId = 10 }, // Tecnología
                    new { CategoryId = 4, ProductId = 10 }, // Celulares
                    new { CategoryId = 7, ProductId = 10 }, // Smartphones

                    // Aire Acondicionado Samsung
                    new { CategoryId = 1, ProductId = 11 }, // Tecnología
                    new { CategoryId = 5, ProductId = 11 }, // Climatización
                    new { CategoryId = 8, ProductId = 11 }, // Aires Acondicionados

                    // Aire Acondicionado Daikin
                    new { CategoryId = 1, ProductId = 12 }, // Tecnología
                    new { CategoryId = 5, ProductId = 12 }, // Climatización
                    new { CategoryId = 8, ProductId = 12 }, // Aires Acondicionados

                    // Horno Microondas Whirlpool
                    new { CategoryId = 1, ProductId = 13 }, // Tecnología
                    new { CategoryId = 6, ProductId = 13 }, // Cocina
                    new { CategoryId = 9, ProductId = 13 }, // Hornos

                    // Horno Eléctrico Oster
                    new { CategoryId = 1, ProductId = 14 }, // Tecnología
                    new { CategoryId = 6, ProductId = 14 }, // Cocina
                    new { CategoryId = 9, ProductId = 14 }, // Hornos

                    // Armario IKEA Pax
                    new { CategoryId = 1, ProductId = 15 }, // Tecnología
                    new { CategoryId = 7, ProductId = 15 }, // Muebles de Dormitorio
                    new { CategoryId = 10, ProductId = 15 }, // Armarios y Closets

                    // Closet Ashley Furniture
                    new { CategoryId = 1, ProductId = 16 }, // Tecnología
                    new { CategoryId = 7, ProductId = 16 }, // Muebles de Dormitorio
                    new { CategoryId = 10, ProductId = 16 }, // Armarios y Closets

                    // Basecama King Size
                    new { CategoryId = 1, ProductId = 17 }, // Tecnología
                    new { CategoryId = 7, ProductId = 17 }, // Muebles de Dormitorio
                    new { CategoryId = 11, ProductId = 17 }, // Camas y Basecamas

                    // Cama Doble Madera
                    new { CategoryId = 1, ProductId = 18 }, // Tecnología
                    new { CategoryId = 7, ProductId = 18 }, // Muebles de Dormitorio
                    new { CategoryId = 11, ProductId = 18 }, // Camas y Basecamas

                    // Llanta Bridgestone 16"
                    new { CategoryId = 1, ProductId = 19 }, // Tecnología
                    new { CategoryId = 9, ProductId = 19 }, // Llantas
                    new { CategoryId = 12, ProductId = 19 }, // Llantas Carro

                    // Llanta Goodyear 17"
                    new { CategoryId = 1, ProductId = 20 }, // Tecnología
                    new { CategoryId = 9, ProductId = 20 }, // Llantas
                    new { CategoryId = 12, ProductId = 20 }, // Llantas Carro

                    // Batería Bosch 12V
                    new { CategoryId = 1, ProductId = 21 }, // Tecnología
                    new { CategoryId = 10, ProductId = 21 }, // Accesorios Carro
                    new { CategoryId = 13, ProductId = 21 }, // Baterías de Auto

                    // Batería de Auto Energizer
                    new { CategoryId = 1, ProductId = 22 }, // Tecnología
                    new { CategoryId = 10, ProductId = 22 }, // Accesorios Carro
                    new { CategoryId = 13, ProductId = 22 }  // Baterías de Auto
                );
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(c => c.Products)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Product>()
            //               .HasMany(e => e.Images)
            //               .WithMany(e => e.Products)
            //               .UsingEntity<ProductsImages>(
            //               l => l.HasOne<ProductImages>().WithMany().HasForeignKey(e => e.ImageId),
            //               r => r.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId)
            //       ).HasKey(pi => new { pi.ProductId, pi.ImageId });
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Properties)
                .WithMany(c => c.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductPropertyValue",
                    r => r.HasOne<PropertyValue>().WithMany().HasForeignKey("PropertyValueId"),
                    l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId")
                ).HasData(
                   new { PropertyValueId = 3, ProductId = 1 }, // 55 pulgadas
                new { PropertyValueId = 7, ProductId = 1 }, // 4K Ultra HD
                new { PropertyValueId = 9, ProductId = 1 }, // LED

                // Televisor LG OLED 65"
                new { PropertyValueId = 4, ProductId = 2 }, // 65 pulgadas
                new { PropertyValueId = 7, ProductId = 2 }, // 4K Ultra HD
                new { PropertyValueId = 10, ProductId = 2 }, // OLED

                // Soporte para TV hasta 55"
                new { PropertyValueId = 3, ProductId = 3 }, // 55 pulgadas
                new { PropertyValueId = 38, ProductId = 3 }, // Metal

                // Cable HDMI 4K 3 metros
                new { PropertyValueId = 7, ProductId = 4 }, // 4K Ultra HD
                new { PropertyValueId = 39, ProductId = 4 }, // Plástico

                // Portátil Lenovo ThinkPad
                new { PropertyValueId = 21, ProductId = 5 }, // Intel Core i5
                new { PropertyValueId = 17, ProductId = 5 }, // 8GB
                new { PropertyValueId = 26, ProductId = 5 }, // 256GB SSD

                // Portátil HP Pavilion
                new { PropertyValueId = 23, ProductId = 6 }, // Ryzen 5
                new { PropertyValueId = 18, ProductId = 6 }, // 16GB
                new { PropertyValueId = 24, ProductId = 6 }, // 512GB SSD

                // Bocina Bose SoundLink
                new { PropertyValueId = 27, ProductId = 7 }, // Wi-Fi 5
                new { PropertyValueId = 29, ProductId = 7 }, // 10 horas

                // Parlante JBL Flip 5
                new { PropertyValueId = 30, ProductId = 8 }, // 15 horas
                new { PropertyValueId = 27, ProductId = 8 }, // Wi-Fi 5

                // iPhone 14 Pro
                new { PropertyValueId = 19, ProductId = 9 }, // 32GB
                new { PropertyValueId = 7, ProductId = 9 },  // 4K Ultra HD

                // Xiaomi Redmi Note 12
                new { PropertyValueId = 19, ProductId = 10 }, // 32GB
                new { PropertyValueId = 6, ProductId = 10 },  // Full HD

                // Aire Acondicionado Samsung
                new { PropertyValueId = 34, ProductId = 11 }, // 12000 BTU
                new { PropertyValueId = 32, ProductId = 11 }, // Eficiencia A+

                // Aire Acondicionado Daikin
                new { PropertyValueId = 34, ProductId = 12 }, // 12000 BTU
                new { PropertyValueId = 31, ProductId = 12 }, // Eficiencia A

                // Horno Microondas Whirlpool
                new { PropertyValueId = 35, ProductId = 13 }, // 2000W
                new { PropertyValueId = 37, ProductId = 13 }, // Madera

                // Horno Eléctrico Oster
                new { PropertyValueId = 35, ProductId = 14 }, // 2000W
                new { PropertyValueId = 38, ProductId = 14 }, // Metal

                // Armario IKEA Pax
                new { PropertyValueId = 37, ProductId = 15 }, // Madera

                // Closet Ashley Furniture
                new { PropertyValueId = 37, ProductId = 16 }, // Madera

                // Basecama King Size
                new { PropertyValueId = 37, ProductId = 17 }, // Madera

                // Cama Doble Madera
                new { PropertyValueId = 37, ProductId = 18 }, // Madera

                // Llanta Bridgestone 16"
                new { PropertyValueId = 38, ProductId = 19 }, // Metal

                // Llanta Goodyear 17"
                new { PropertyValueId = 38, ProductId = 20 }, // Metal

                // Batería Bosch 12V
                new { PropertyValueId = 35, ProductId = 21 }, // 2000W

                // Batería de Auto Energizer
                new { PropertyValueId = 36, ProductId = 22 }  // 5000W
                );

            modelBuilder.Entity<PropertyType>()
                .HasMany(e => e.Properties)
                .WithOne(p => p.PropertyType)
                .HasForeignKey(e => e.PropertyTypeId)
                .IsRequired();
            modelBuilder.Entity<Property>()
                .HasMany(p => p.PropertyValues)
                .WithOne(pv => pv.Property)
                .HasForeignKey(pv => pv.PropertyId)
                .IsRequired();

         modelBuilder.Entity<Product>().HasData(
    new Product { Id = 1, Name = "Smart TV Samsung 55\"", Current_price = 2500000, Previous_price = 3000000, Description = "Televisor Samsung de 55 pulgadas con resolución 4K y tecnología HDR.", BrandId = 1 },
    new Product { Id = 2, Name = "Televisor LG OLED 65\"", Current_price = 4500000, Previous_price = 5000000, Description = "Televisor LG OLED de 65 pulgadas con tecnología AI ThinQ.", BrandId = 2 },
    new Product { Id = 3, Name = "Soporte para TV hasta 55\"", Current_price = 100000, Previous_price = 150000, Description = "Soporte universal para televisores de hasta 55 pulgadas, fácil instalación.", BrandId = 3 },
    new Product { Id = 4, Name = "Cable HDMI 4K 3 metros", Current_price = 50000, Previous_price = 80000, Description = "Cable HDMI 2.1 de 3 metros, compatible con resolución 4K y 8K.", BrandId = 4 },
    new Product { Id = 5, Name = "Portátil Lenovo ThinkPad", Current_price = 3200000, Previous_price = 3500000, Description = "Portátil Lenovo ThinkPad con procesador Intel Core i5 y 8GB de RAM.", BrandId = 5 },
    new Product { Id = 6, Name = "Portátil HP Pavilion", Current_price = 2800000, Previous_price = 3200000, Description = "Portátil HP Pavilion con pantalla Full HD y disco SSD de 256GB.", BrandId = 6 },
    new Product { Id = 7, Name = "Bocina Bose SoundLink", Current_price = 600000, Previous_price = 700000, Description = "Parlante portátil Bose con conexión Bluetooth y sonido premium.", BrandId = 8 },
    new Product { Id = 8, Name = "Parlante JBL Flip 5", Current_price = 450000, Previous_price = 500000, Description = "Parlante portátil JBL con batería de larga duración y diseño resistente al agua.", BrandId = 9 },
    new Product { Id = 9, Name = "iPhone 14 Pro", Current_price = 5500000, Previous_price = 6000000, Description = "Smartphone Apple iPhone 14 Pro con cámara de 48 MP y 256GB de almacenamiento.", BrandId = 10 },
    new Product { Id = 10, Name = "Xiaomi Redmi Note 12", Current_price = 1200000, Previous_price = 1500000, Description = "Smartphone Xiaomi con pantalla AMOLED y procesador Snapdragon 685.", BrandId = 11 },
    new Product { Id = 11, Name = "Aire Acondicionado Samsung", Current_price = 1800000, Previous_price = 2000000, Description = "Aire acondicionado Samsung de 12000 BTU, ideal para habitaciones medianas.", BrandId = 14 },
    new Product { Id = 12, Name = "Aire Acondicionado Daikin", Current_price = 2400000, Previous_price = 2800000, Description = "Aire acondicionado Daikin con tecnología inverter y bajo consumo energético.", BrandId = 13 },
    new Product { Id = 13, Name = "Horno Microondas Whirlpool", Current_price = 350000, Previous_price = 450000, Description = "Horno microondas Whirlpool de 20 litros con función grill.", BrandId = 12 },
    new Product { Id = 14, Name = "Horno Eléctrico Oster", Current_price = 400000, Previous_price = 500000, Description = "Horno eléctrico Oster de 25 litros, ideal para hornear y tostar.", BrandId = 14 },
    new Product { Id = 15, Name = "Armario IKEA Pax", Current_price = 1500000, Previous_price = 1800000, Description = "Armario IKEA Pax modular con diseño minimalista y amplio espacio de almacenamiento.", BrandId = 15 },
    new Product { Id = 16, Name = "Closet Ashley Furniture", Current_price = 2000000, Previous_price = 2300000, Description = "Closet Ashley Furniture con puertas corredizas y diseño elegante.", BrandId = 16 },
    new Product { Id = 17, Name = "Basecama King Size", Current_price = 800000, Previous_price = 1000000, Description = "Basecama king size con estructura resistente y diseño moderno.", BrandId = 16 },
    new Product { Id = 18, Name = "Cama Doble Madera", Current_price = 1200000, Previous_price = 1400000, Description = "Cama doble de madera maciza con acabado natural.", BrandId = 15 },
    new Product { Id = 19, Name = "Llanta Bridgestone 16\"", Current_price = 350000, Previous_price = 400000, Description = "Llanta Bridgestone para automóvil, tamaño 16 pulgadas.", BrandId = 17 },
    new Product { Id = 20, Name = "Llanta Goodyear 17\"", Current_price = 400000, Previous_price = 450000, Description = "Llanta Goodyear para SUV, tamaño 17 pulgadas.", BrandId = 18 },
    new Product { Id = 21, Name = "Batería Bosch 12V", Current_price = 500000, Previous_price = 600000, Description = "Batería para automóvil Bosch con capacidad de 12V y 70Ah.", BrandId = 20 },
    new Product { Id = 22, Name = "Batería de Auto Energizer", Current_price = 450000, Previous_price = 550000, Description = "Batería de auto Energizer con alto rendimiento y larga duración.", BrandId = 19 }
);
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, BrandName = "Samsung" },
                new Brand { Id = 2, BrandName = "LG" },
                new Brand { Id = 3, BrandName = "Sony" },
                new Brand { Id = 4, BrandName = "Panasonic" },
                new Brand { Id = 5, BrandName = "Lenovo" },
                new Brand { Id = 6, BrandName = "HP" },
                new Brand { Id = 7, BrandName = "Dell" },
                new Brand { Id = 8, BrandName = "Bose" },
                new Brand { Id = 9, BrandName = "JBL" },
                new Brand { Id = 10, BrandName = "Apple" },
                new Brand { Id = 11, BrandName = "Xiaomi" },
                new Brand { Id = 12, BrandName = "Whirlpool" },
                new Brand { Id = 13, BrandName = "Daikin" },
                new Brand { Id = 14, BrandName = "Samsung Appliances" },
                new Brand { Id = 15, BrandName = "IKEA" },
                new Brand { Id = 16, BrandName = "Ashley Furniture" },
                new Brand { Id = 17, BrandName = "Bridgestone" },
                new Brand { Id = 18, BrandName = "Goodyear" },
                new Brand { Id = 19, BrandName = "Michelin" },
                new Brand { Id = 20, BrandName = "Bosch" }
            );

           modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Tecnologia", CategoryLevel = 0, CategoryParentId = 0 },
            new Category { Id = 2, CategoryName = "TV, Audio y Video", CategoryLevel = 1, CategoryParentId = 1 },
            new Category { Id = 3, CategoryName = "Televisores", CategoryLevel = 2, CategoryParentId = 2 },
            new Category { Id = 4, CategoryName = "Accesorios TV", CategoryLevel = 2, CategoryParentId = 2 },
            new Category { Id = 5, CategoryName = "Computadores y Accesorios", CategoryLevel = 1, CategoryParentId = 1 },
            new Category { Id = 6, CategoryName = "Computadores Portátiles", CategoryLevel = 2, CategoryParentId = 5 },
            new Category { Id = 7, CategoryName = "Audio y Video", CategoryLevel = 1, CategoryParentId = 1 },
            new Category { Id = 8, CategoryName = "Parlantes", CategoryLevel = 2, CategoryParentId = 7 },
            new Category { Id = 9, CategoryName = "Celulares", CategoryLevel = 1, CategoryParentId = 1 },
            new Category { Id = 10, CategoryName = "Smartphones", CategoryLevel = 2, CategoryParentId = 9 },
            new Category { Id = 11, CategoryName = "Electrodomésticos", CategoryLevel = 0, CategoryParentId = 0 },
            new Category { Id = 12, CategoryName = "Climatización", CategoryLevel = 1, CategoryParentId = 11 },
            new Category { Id = 13, CategoryName = "Aires Acondicionados", CategoryLevel = 2, CategoryParentId = 12 },
            new Category { Id = 14, CategoryName = "Cocina", CategoryLevel = 1, CategoryParentId = 11 },
            new Category { Id = 15, CategoryName = "Hornos", CategoryLevel = 2, CategoryParentId = 14 },
            new Category { Id = 16, CategoryName = "Muebles", CategoryLevel = 0, CategoryParentId = 0 },
            new Category { Id = 17, CategoryName = "Muebles de Dormitorio", CategoryLevel = 1, CategoryParentId = 16 },
            new Category { Id = 18, CategoryName = "Armarios y Closets", CategoryLevel = 2, CategoryParentId = 17 },
            new Category { Id = 19, CategoryName = "Camas y Basecamas", CategoryLevel = 2, CategoryParentId = 17 },
            new Category { Id = 20, CategoryName = "Automóvil", CategoryLevel = 0, CategoryParentId = 0 },
            new Category { Id = 21, CategoryName = "Llantas", CategoryLevel = 1, CategoryParentId = 20 },
            new Category { Id = 22, CategoryName = "Llantas Carro", CategoryLevel = 2, CategoryParentId = 21 },
            new Category { Id = 23, CategoryName = "Accesorios Carro", CategoryLevel = 1, CategoryParentId = 20 },
            new Category { Id = 24, CategoryName = "Batería de Auto", CategoryLevel = 2, CategoryParentId = 23 }
        );

        modelBuilder.Entity<ProductImages>().HasData(
            new ProductImages
            {
                Id = 1, ProductId = 1, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 2, ProductId = 2, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 3, ProductId = 3, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 4, ProductId = 4, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 5, ProductId = 5, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 6, ProductId = 6, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 7, ProductId = 7, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 8, ProductId = 8, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 9, ProductId = 9, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 10, ProductId = 10, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 11, ProductId = 11, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 12, ProductId = 12, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 13, ProductId = 13, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 14, ProductId = 14, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 15, ProductId = 15, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 16, ProductId = 16, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 17, ProductId = 17, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 18, ProductId = 18, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 19, ProductId = 19, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 20, ProductId = 20, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 21, ProductId = 21, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            },
            new ProductImages
            {
                Id = 22, ProductId = 22, ImageLabel = "image1",
                ImageUrl =
                    "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
            }
        );
           

            //          modelBuilder.Entity<ProductImages>().HasData(
            //    new ProductImages { ImageId = 1, ProductId = 2 },
            //    new ProductImages { ImageId = 2, ProductId = 2 },
            //    new ProductImages { ImageId = 3, ProductId = 1 },
            //    new ProductImages { ImageId = 4, ProductId = 3 },
            //    new ProductImages { ImageId = 5, ProductId = 3 },
            //    new ProductImages { ImageId = 6, ProductId = 4 },
            //    new ProductImages { ImageId = 7, ProductId = 4 }
            //);
            //        modelBuilder.Entity<ProductsCategories>().HasData(
            //new ProductsCategories { CategoryId = 1, ProductId = 2 },
            //new ProductsCategories { CategoryId = 2, ProductId = 2 },
            //new ProductsCategories { CategoryId = 3, ProductId = 2 },
            //new ProductsCategories { CategoryId = 1, ProductId = 3 },
            //new ProductsCategories { CategoryId = 2, ProductId = 3 },
            //new ProductsCategories { CategoryId = 3, ProductId = 3 },
            //new ProductsCategories { CategoryId = 1, ProductId = 1 },
            //new ProductsCategories { CategoryId = 2, ProductId = 1 },
            //new ProductsCategories { CategoryId = 3, ProductId = 1 },
            //new ProductsCategories { CategoryId = 1, ProductId = 4 },
            //new ProductsCategories { CategoryId = 2, ProductId = 4 },
            //new ProductsCategories { CategoryId = 3, ProductId = 4 }


            //);
            //////////////////////////////////////

            modelBuilder.Entity<PropertyType>().HasData(new PropertyType
            {
                Id = 1,
                PropertyTypeName = "Caracteristicas Principales",
                PropertyTypeDescription = "Caracteristicas mas destacables de los productos",
                PropertyTypeOrder = 1,
            });
            modelBuilder.Entity<PropertyType>().HasData(new PropertyType
            {
                Id = 2,
                PropertyTypeName = "Funciones",
                PropertyTypeDescription = "Funciones principales de los productos",
                PropertyTypeOrder = 2,
            });
            modelBuilder.Entity<PropertyType>().HasData(new PropertyType
            {
                Id = 3,
                PropertyTypeName = "Componentes",
                PropertyTypeDescription = "Componentes que componen el producto",
                PropertyTypeOrder = 3,
            });
            ////////////////////////////////////

       modelBuilder.Entity<Property>().HasData(
        new Property { Id = 1, PropertyName = "Tamaño de Pantalla", PropertyTypeId = 1, PropertyOrder = 1 },
        new Property { Id = 2, PropertyName = "Resolución", PropertyTypeId = 1, PropertyOrder = 2 },
        new Property { Id = 3, PropertyName = "Tipo de Pantalla", PropertyTypeId = 1, PropertyOrder = 3 },
        new Property { Id = 4, PropertyName = "HDMI", PropertyTypeId = 2, PropertyOrder = 1 },
        new Property { Id = 5, PropertyName = "Conectividad Bluetooth", PropertyTypeId = 2, PropertyOrder = 2 },
        new Property { Id = 6, PropertyName = "HDR", PropertyTypeId = 2, PropertyOrder = 3 },
        new Property { Id = 7, PropertyName = "Control Remoto", PropertyTypeId = 3, PropertyOrder = 1 },
        new Property { Id = 8, PropertyName = "Puertos HDMI", PropertyTypeId = 3, PropertyOrder = 2 },
        new Property { Id = 9, PropertyName = "Memoria RAM", PropertyTypeId = 1, PropertyOrder = 4 },
        new Property { Id = 10, PropertyName = "Procesador", PropertyTypeId = 1, PropertyOrder = 5 },
        new Property { Id = 11, PropertyName = "Sistema Operativo", PropertyTypeId = 1, PropertyOrder = 6 },
        new Property { Id = 12, PropertyName = "Duración de Batería", PropertyTypeId = 2, PropertyOrder = 4 },
        new Property { Id = 13, PropertyName = "Conectividad Wi-Fi", PropertyTypeId = 2, PropertyOrder = 5 },
        new Property { Id = 14, PropertyName = "Puertos USB", PropertyTypeId = 3, PropertyOrder = 3 },
        new Property { Id = 15, PropertyName = "Capacidad de Almacenamiento", PropertyTypeId = 1, PropertyOrder = 7 },
        new Property { Id = 16, PropertyName = "Cámara", PropertyTypeId = 2, PropertyOrder = 6 },
        new Property { Id = 17, PropertyName = "Carga Rápida", PropertyTypeId = 2, PropertyOrder = 7 },
        new Property { Id = 18, PropertyName = "Tecnología Inverter", PropertyTypeId = 2, PropertyOrder = 8 },
        new Property { Id = 19, PropertyName = "Eficiencia Energética", PropertyTypeId = 1, PropertyOrder = 8 },
        new Property { Id = 20, PropertyName = "Capacidad", PropertyTypeId = 1, PropertyOrder = 9 },
        new Property { Id = 21, PropertyName = "Programas de Cocción", PropertyTypeId = 2, PropertyOrder = 9 },
        new Property { Id = 22, PropertyName = "Material de la Cava", PropertyTypeId = 3, PropertyOrder = 4 },
        new Property { Id = 23, PropertyName = "Potencia de Salida", PropertyTypeId = 1, PropertyOrder = 10 },
        new Property { Id = 24, PropertyName = "Impermeable", PropertyTypeId = 2, PropertyOrder = 10 },
        new Property { Id = 25, PropertyName = "Dimensiones", PropertyTypeId = 1, PropertyOrder = 11 },
        new Property { Id = 26, PropertyName = "Material", PropertyTypeId = 3, PropertyOrder = 5 }
        );
            /////////////////////////////////////

          modelBuilder.Entity<PropertyValue>().HasData(
    new PropertyValue { Id = 1, PropertyValueName = "32 pulgadas", PropertyId = 1 },
    new PropertyValue { Id = 2, PropertyValueName = "43 pulgadas", PropertyId = 1 },
    new PropertyValue { Id = 3, PropertyValueName = "55 pulgadas", PropertyId = 1 },
    new PropertyValue { Id = 4, PropertyValueName = "65 pulgadas", PropertyId = 1 },
    new PropertyValue { Id = 5, PropertyValueName = "HD", PropertyId = 2 },
    new PropertyValue { Id = 6, PropertyValueName = "Full HD", PropertyId = 2 },
    new PropertyValue { Id = 7, PropertyValueName = "4K Ultra HD", PropertyId = 2 },
    new PropertyValue { Id = 8, PropertyValueName = "8K Ultra HD", PropertyId = 2 },
    new PropertyValue { Id = 9, PropertyValueName = "LED", PropertyId = 3 },
    new PropertyValue { Id = 10, PropertyValueName = "OLED", PropertyId = 3 },
    new PropertyValue { Id = 11, PropertyValueName = "QLED", PropertyId = 3 },
    new PropertyValue { Id = 12, PropertyValueName = "IPS", PropertyId = 3 },
    new PropertyValue { Id = 13, PropertyValueName = "2 Puertos", PropertyId = 8 },
    new PropertyValue { Id = 14, PropertyValueName = "3 Puertos", PropertyId = 8 },
    new PropertyValue { Id = 15, PropertyValueName = "4 Puertos", PropertyId = 8 },
    new PropertyValue { Id = 16, PropertyValueName = "4GB", PropertyId = 9 },
    new PropertyValue { Id = 17, PropertyValueName = "8GB", PropertyId = 9 },
    new PropertyValue { Id = 18, PropertyValueName = "16GB", PropertyId = 9 },
    new PropertyValue { Id = 19, PropertyValueName = "32GB", PropertyId = 9 },
    new PropertyValue { Id = 20, PropertyValueName = "Intel Core i3", PropertyId = 10 },
    new PropertyValue { Id = 21, PropertyValueName = "Intel Core i5", PropertyId = 10 },
    new PropertyValue { Id = 22, PropertyValueName = "Intel Core i7", PropertyId = 10 },
    new PropertyValue { Id = 23, PropertyValueName = "Ryzen 5", PropertyId = 10 },
    new PropertyValue { Id = 24, PropertyValueName = "512GB SSD", PropertyId = 15 },
    new PropertyValue { Id = 25, PropertyValueName = "1TB HDD", PropertyId = 15 },
    new PropertyValue { Id = 26, PropertyValueName = "256GB SSD", PropertyId = 15 },
    new PropertyValue { Id = 27, PropertyValueName = "Wi-Fi 5", PropertyId = 13 },
    new PropertyValue { Id = 28, PropertyValueName = "Wi-Fi 6", PropertyId = 13 },
    new PropertyValue { Id = 29, PropertyValueName = "10 horas", PropertyId = 12 },
    new PropertyValue { Id = 30, PropertyValueName = "15 horas", PropertyId = 12 },
    new PropertyValue { Id = 31, PropertyValueName = "Eficiencia A", PropertyId = 19 },
    new PropertyValue { Id = 32, PropertyValueName = "Eficiencia A+", PropertyId = 19 },
    new PropertyValue { Id = 33, PropertyValueName = "5000 BTU", PropertyId = 20 },
    new PropertyValue { Id = 34, PropertyValueName = "12000 BTU", PropertyId = 20 },
    new PropertyValue { Id = 35, PropertyValueName = "2000W", PropertyId = 23 },
    new PropertyValue { Id = 36, PropertyValueName = "5000W", PropertyId = 23 },
    new PropertyValue { Id = 37, PropertyValueName = "Madera", PropertyId = 26 },
    new PropertyValue { Id = 38, PropertyValueName = "Metal", PropertyId = 26 },
    new PropertyValue { Id = 39, PropertyValueName = "Plástico", PropertyId = 26 }
);
        }
    }
}