﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using microStore.Services.ProductApi.Data;

#nullable disable

namespace microStore.Services.ProductApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241218154954_ModifyProperties")]
    partial class ModifyProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 4
                        });
                });

            modelBuilder.Entity("ProductImage", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ImageId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            ProductId = 2
                        },
                        new
                        {
                            ImageId = 2,
                            ProductId = 2
                        },
                        new
                        {
                            ImageId = 3,
                            ProductId = 1
                        },
                        new
                        {
                            ImageId = 4,
                            ProductId = 3
                        },
                        new
                        {
                            ImageId = 5,
                            ProductId = 3
                        },
                        new
                        {
                            ImageId = 6,
                            ProductId = 4
                        },
                        new
                        {
                            ImageId = 7,
                            ProductId = 4
                        });
                });

            modelBuilder.Entity("ProductPropertyValue", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyValueId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "PropertyValueId");

                    b.HasIndex("PropertyValueId");

                    b.ToTable("ProductPropertyValue");

                    b.HasData(
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 23
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 21
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 19
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 15
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 14
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 12
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 8
                        },
                        new
                        {
                            ProductId = 3,
                            PropertyValueId = 3
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandName = "Brand 1"
                        },
                        new
                        {
                            Id = 2,
                            BrandName = "Brand 2"
                        },
                        new
                        {
                            Id = 3,
                            BrandName = "Brand 3"
                        },
                        new
                        {
                            Id = 4,
                            BrandName = "Brand 4"
                        },
                        new
                        {
                            Id = 5,
                            BrandName = "Brand 5"
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryLevel")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryLevel = 0,
                            CategoryName = "Cat 1",
                            CategoryParentId = 0
                        },
                        new
                        {
                            Id = 2,
                            CategoryLevel = 1,
                            CategoryName = "Cat 2",
                            CategoryParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            CategoryLevel = 2,
                            CategoryName = "Cat 3",
                            CategoryParentId = 2
                        },
                        new
                        {
                            Id = 4,
                            CategoryLevel = 1,
                            CategoryName = "Cat 4",
                            CategoryParentId = 1
                        },
                        new
                        {
                            Id = 5,
                            CategoryLevel = 2,
                            CategoryName = "Cat 5",
                            CategoryParentId = 4
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<decimal>("Current_price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Previous_price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            Current_price = 3000000m,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            IsAvailable = true,
                            Link = "samosa-1",
                            Name = "Samosa",
                            Previous_price = 3500000m
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            Current_price = 3000000m,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            IsAvailable = true,
                            Link = "paneer-tikka-2",
                            Name = "Paneer Tikka",
                            Previous_price = 3500000m
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 3,
                            Current_price = 3000000m,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            IsAvailable = true,
                            Link = "sweet-pie-3",
                            Name = "Sweet Pie",
                            Previous_price = 3500000m
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 4,
                            Current_price = 3000000m,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            IsAvailable = true,
                            Link = "pav-bhaji-4",
                            Name = "Pav Bhaji",
                            Previous_price = 3500000m
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.ProductImages", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<string>("ImageLabel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            ImageId = 1,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        },
                        new
                        {
                            ImageId = 7,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        },
                        new
                        {
                            ImageId = 2,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        },
                        new
                        {
                            ImageId = 3,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        },
                        new
                        {
                            ImageId = 4,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        },
                        new
                        {
                            ImageId = 5,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        },
                        new
                        {
                            ImageId = 6,
                            ImageLabel = "image1",
                            ImageUrl = "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA"
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"));

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsPrincipal")
                        .HasColumnType("bit");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyOrder")
                        .HasColumnType("int");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("int");

                    b.HasKey("PropertyId");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            PropertyId = 1,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Memoria Interna",
                            PropertyOrder = 1,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 2,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Memoria RAM",
                            PropertyOrder = 2,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 3,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Modelo del Procesador",
                            PropertyOrder = 2,
                            PropertyTypeId = 3
                        },
                        new
                        {
                            PropertyId = 4,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Frecuencia",
                            PropertyOrder = 2,
                            PropertyTypeId = 3
                        },
                        new
                        {
                            PropertyId = 5,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Tipo de pantalla",
                            PropertyOrder = 3,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 6,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Resolucion",
                            PropertyOrder = 4,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 7,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Tamaño bateria",
                            PropertyOrder = 3,
                            PropertyTypeId = 3
                        },
                        new
                        {
                            PropertyId = 8,
                            Id = 0,
                            IsPrincipal = false,
                            PropertyName = "Bateria",
                            PropertyOrder = 4,
                            PropertyTypeId = 3
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.PropertyType", b =>
                {
                    b.Property<int>("PropertyTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyTypeId"));

                    b.Property<string>("PropertyTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyTypeOrder")
                        .HasColumnType("int");

                    b.HasKey("PropertyTypeId");

                    b.ToTable("PropertyTypes");

                    b.HasData(
                        new
                        {
                            PropertyTypeId = 1,
                            PropertyTypeDescription = "Caracteristicas mas destacables de los productos",
                            PropertyTypeName = "Caracteristicas Principales",
                            PropertyTypeOrder = 1
                        },
                        new
                        {
                            PropertyTypeId = 2,
                            PropertyTypeDescription = "Funciones principales de los productos",
                            PropertyTypeName = "Funciones",
                            PropertyTypeOrder = 2
                        },
                        new
                        {
                            PropertyTypeId = 3,
                            PropertyTypeDescription = "Componentes que componen el producto",
                            PropertyTypeName = "Componentes",
                            PropertyTypeOrder = 3
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.PropertyValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyValueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyValues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PropertyId = 1,
                            PropertyValueName = "512GB"
                        },
                        new
                        {
                            Id = 2,
                            PropertyId = 1,
                            PropertyValueName = "256GB"
                        },
                        new
                        {
                            Id = 3,
                            PropertyId = 1,
                            PropertyValueName = "128GB"
                        },
                        new
                        {
                            Id = 4,
                            PropertyId = 1,
                            PropertyValueName = "64GB"
                        },
                        new
                        {
                            Id = 5,
                            PropertyId = 1,
                            PropertyValueName = "1000GB"
                        },
                        new
                        {
                            Id = 6,
                            PropertyId = 2,
                            PropertyValueName = "8GB"
                        },
                        new
                        {
                            Id = 7,
                            PropertyId = 2,
                            PropertyValueName = "4GB"
                        },
                        new
                        {
                            Id = 8,
                            PropertyId = 2,
                            PropertyValueName = "6GB"
                        },
                        new
                        {
                            Id = 9,
                            PropertyId = 2,
                            PropertyValueName = "12GB"
                        },
                        new
                        {
                            Id = 10,
                            PropertyId = 2,
                            PropertyValueName = "16GB"
                        },
                        new
                        {
                            Id = 11,
                            PropertyId = 3,
                            PropertyValueName = "Mediatek"
                        },
                        new
                        {
                            Id = 12,
                            PropertyId = 3,
                            PropertyValueName = "Qualcomm"
                        },
                        new
                        {
                            Id = 13,
                            PropertyId = 4,
                            PropertyValueName = "3.3 GHz"
                        },
                        new
                        {
                            Id = 14,
                            PropertyId = 4,
                            PropertyValueName = "2.8 GHz"
                        },
                        new
                        {
                            Id = 15,
                            PropertyId = 5,
                            PropertyValueName = "AMOLED"
                        },
                        new
                        {
                            Id = 16,
                            PropertyId = 5,
                            PropertyValueName = "LED"
                        },
                        new
                        {
                            Id = 18,
                            PropertyId = 6,
                            PropertyValueName = "1080 x 2400 px • FHD+"
                        },
                        new
                        {
                            Id = 19,
                            PropertyId = 6,
                            PropertyValueName = "1440 x 3168 px • WQHD+"
                        },
                        new
                        {
                            Id = 20,
                            PropertyId = 7,
                            PropertyValueName = "6.55\""
                        },
                        new
                        {
                            Id = 21,
                            PropertyId = 7,
                            PropertyValueName = "6.82\""
                        },
                        new
                        {
                            Id = 22,
                            PropertyId = 8,
                            PropertyValueName = "5000 mAh"
                        },
                        new
                        {
                            Id = 23,
                            PropertyId = 8,
                            PropertyValueName = "4000 mAh"
                        });
                });

            modelBuilder.Entity("ProductCategory", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("microStore.Services.ProductApi.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductImage", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.ProductImages", null)
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("microStore.Services.ProductApi.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductPropertyValue", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("microStore.Services.ProductApi.Models.PropertyValue", null)
                        .WithMany()
                        .HasForeignKey("PropertyValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Product", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.Brand", "Brand")
                        .WithOne()
                        .HasForeignKey("microStore.Services.ProductApi.Models.Product", "BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Property", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.PropertyType", null)
                        .WithMany("Properties")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.PropertyValue", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.Property", "Property")
                        .WithMany("PropertyValues")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Property", b =>
                {
                    b.Navigation("PropertyValues");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.PropertyType", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
