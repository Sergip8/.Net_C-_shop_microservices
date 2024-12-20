﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using microStore.Services.CommentApi.Data;

#nullable disable

namespace microStore.Services.CommentApi.Migrations
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

            modelBuilder.Entity("microStore.Services.CommentApi.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<int>("CommentHeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Votes")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("CommentHeaderId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            CommentId = 1,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5579),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        },
                        new
                        {
                            CommentId = 2,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5602),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        },
                        new
                        {
                            CommentId = 3,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5615),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        },
                        new
                        {
                            CommentId = 4,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5628),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        },
                        new
                        {
                            CommentId = 5,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5640),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        },
                        new
                        {
                            CommentId = 6,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5673),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        },
                        new
                        {
                            CommentId = 7,
                            CommentHeaderId = 1,
                            Content = "Una mierda pinchada en un palo",
                            CreatedDate = new DateTime(2024, 12, 1, 14, 5, 29, 360, DateTimeKind.Local).AddTicks(5686),
                            Score = 1,
                            Title = "Comment",
                            Votes = 0
                        });
                });

            modelBuilder.Entity("microStore.Services.CommentApi.Models.CommentHeader", b =>
                {
                    b.Property<int>("CommentHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentHeaderId"));

                    b.Property<int>("CommentCount")
                        .HasColumnType("int");

                    b.Property<float>("OverallScore")
                        .HasColumnType("real");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("QtyForStar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommentHeaderId");

                    b.ToTable("CommentHeader");

                    b.HasData(
                        new
                        {
                            CommentHeaderId = 1,
                            CommentCount = 7,
                            OverallScore = 2f,
                            ProductId = 20,
                            QtyForStar = ""
                        });
                });

            modelBuilder.Entity("microStore.Services.CommentApi.Models.Comment", b =>
                {
                    b.HasOne("microStore.Services.CommentApi.Models.CommentHeader", null)
                        .WithMany("Comments")
                        .HasForeignKey("CommentHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("microStore.Services.CommentApi.Models.CommentHeader", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
