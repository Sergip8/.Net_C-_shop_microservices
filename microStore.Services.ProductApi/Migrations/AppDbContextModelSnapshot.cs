﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using microStore.Services.ProductApi.Data;

#nullable disable

namespace microStore.Services.ProductApi.Migrations
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

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            BrandId = 1,
                            BrandName = "Brand 1"
                        },
                        new
                        {
                            BrandId = 2,
                            BrandName = "Brand 2"
                        },
                        new
                        {
                            BrandId = 3,
                            BrandName = "Brand 3"
                        },
                        new
                        {
                            BrandId = 4,
                            BrandName = "Brand 4"
                        },
                        new
                        {
                            BrandId = 5,
                            BrandName = "Brand 5"
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<int>("CategoryLevel")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryParentId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryLevel = 0,
                            CategoryName = "Cat 1",
                            CategoryParentId = 0
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryLevel = 1,
                            CategoryName = "Cat 2",
                            CategoryParentId = 1
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryLevel = 2,
                            CategoryName = "Cat 3",
                            CategoryParentId = 2
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryLevel = 1,
                            CategoryName = "Cat 4",
                            CategoryParentId = 1
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryLevel = 2,
                            CategoryName = "Cat 5",
                            CategoryParentId = 4
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.PriceRange", b =>
                {
                    b.Property<int>("PriceRangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceRangeId"));

                    b.Property<decimal>("HighPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LowPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PriceRangeId");

                    b.ToTable("PriceRanges");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceRangeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            BrandId = 1,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            Link = "",
                            Name = "Samosa",
                            PriceRangeId = 0
                        },
                        new
                        {
                            ProductId = 2,
                            BrandId = 2,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            Link = "",
                            Name = "Paneer Tikka",
                            PriceRangeId = 0
                        },
                        new
                        {
                            ProductId = 3,
                            BrandId = 3,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            Link = "",
                            Name = "Sweet Pie",
                            PriceRangeId = 0
                        },
                        new
                        {
                            ProductId = 4,
                            BrandId = 4,
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            Link = "",
                            Name = "Pav Bhaji",
                            PriceRangeId = 0
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

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.ProductPropertyValue", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyValueId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "PropertyValueId");

                    b.HasIndex("PropertyValueId");

                    b.ToTable("ProductPropertyValues");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.ProductsCategories", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsCategories");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyId"));

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
                            IsPrincipal = false,
                            PropertyName = "Memoria Interna",
                            PropertyOrder = 1,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 2,
                            IsPrincipal = false,
                            PropertyName = "Memoria RAM",
                            PropertyOrder = 2,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 3,
                            IsPrincipal = false,
                            PropertyName = "Modelo del Procesador",
                            PropertyOrder = 2,
                            PropertyTypeId = 3
                        },
                        new
                        {
                            PropertyId = 4,
                            IsPrincipal = false,
                            PropertyName = "Frecuencia",
                            PropertyOrder = 2,
                            PropertyTypeId = 3
                        },
                        new
                        {
                            PropertyId = 5,
                            IsPrincipal = false,
                            PropertyName = "Tipo de pantalla",
                            PropertyOrder = 3,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 6,
                            IsPrincipal = false,
                            PropertyName = "Resolucion",
                            PropertyOrder = 4,
                            PropertyTypeId = 1
                        },
                        new
                        {
                            PropertyId = 7,
                            IsPrincipal = false,
                            PropertyName = "Tamaño bateria",
                            PropertyOrder = 3,
                            PropertyTypeId = 3
                        },
                        new
                        {
                            PropertyId = 8,
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
                    b.Property<int>("PropertyValueId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyValueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyValueId");

                    b.ToTable("PropertyValues");

                    b.HasData(
                        new
                        {
                            PropertyValueId = 1,
                            PropertyId = 1,
                            PropertyValueName = "512GB"
                        },
                        new
                        {
                            PropertyValueId = 2,
                            PropertyId = 1,
                            PropertyValueName = "256GB"
                        },
                        new
                        {
                            PropertyValueId = 3,
                            PropertyId = 1,
                            PropertyValueName = "128GB"
                        },
                        new
                        {
                            PropertyValueId = 4,
                            PropertyId = 1,
                            PropertyValueName = "64GB"
                        },
                        new
                        {
                            PropertyValueId = 5,
                            PropertyId = 1,
                            PropertyValueName = "1000GB"
                        },
                        new
                        {
                            PropertyValueId = 6,
                            PropertyId = 2,
                            PropertyValueName = "8GB"
                        },
                        new
                        {
                            PropertyValueId = 7,
                            PropertyId = 2,
                            PropertyValueName = "4GB"
                        },
                        new
                        {
                            PropertyValueId = 8,
                            PropertyId = 2,
                            PropertyValueName = "6GB"
                        },
                        new
                        {
                            PropertyValueId = 9,
                            PropertyId = 2,
                            PropertyValueName = "12GB"
                        },
                        new
                        {
                            PropertyValueId = 10,
                            PropertyId = 2,
                            PropertyValueName = "16GB"
                        },
                        new
                        {
                            PropertyValueId = 11,
                            PropertyId = 3,
                            PropertyValueName = "Mediatek"
                        },
                        new
                        {
                            PropertyValueId = 12,
                            PropertyId = 3,
                            PropertyValueName = "Qualcomm"
                        },
                        new
                        {
                            PropertyValueId = 13,
                            PropertyId = 4,
                            PropertyValueName = "3.3 GHz"
                        },
                        new
                        {
                            PropertyValueId = 14,
                            PropertyId = 4,
                            PropertyValueName = "2.8 GHz"
                        },
                        new
                        {
                            PropertyValueId = 15,
                            PropertyId = 5,
                            PropertyValueName = "AMOLED"
                        },
                        new
                        {
                            PropertyValueId = 16,
                            PropertyId = 5,
                            PropertyValueName = "LED"
                        },
                        new
                        {
                            PropertyValueId = 18,
                            PropertyId = 6,
                            PropertyValueName = "1080 x 2400 px • FHD+"
                        },
                        new
                        {
                            PropertyValueId = 19,
                            PropertyId = 6,
                            PropertyValueName = "1440 x 3168 px • WQHD+"
                        },
                        new
                        {
                            PropertyValueId = 20,
                            PropertyId = 7,
                            PropertyValueName = "6.55\""
                        },
                        new
                        {
                            PropertyValueId = 21,
                            PropertyId = 7,
                            PropertyValueName = "6.82\""
                        },
                        new
                        {
                            PropertyValueId = 22,
                            PropertyId = 8,
                            PropertyValueName = "5000 mAh"
                        },
                        new
                        {
                            PropertyValueId = 23,
                            PropertyId = 8,
                            PropertyValueName = "4000 mAh"
                        });
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.ProductImages", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.ProductPropertyValue", b =>
                {
                    b.HasOne("microStore.Services.ProductApi.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("microStore.Services.ProductApi.Models.PropertyValue", "PropertyValue")
                        .WithMany()
                        .HasForeignKey("PropertyValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("PropertyValue");
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.ProductsCategories", b =>
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
                    b.HasOne("microStore.Services.ProductApi.Models.Property", null)
                        .WithMany("PropertyValues")
                        .HasForeignKey("PropertyValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("microStore.Services.ProductApi.Models.Product", b =>
                {
                    b.Navigation("Images");
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
