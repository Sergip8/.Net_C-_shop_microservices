using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "ActivationCount", "CouponCode", "DiscountAmount", "ExpirationDate", "MinAmount" },
                values: new object[,]
                {
                    { 1, 1, "Puto1", 30, new DateTime(2024, 4, 8, 20, 39, 11, 511, DateTimeKind.Local).AddTicks(8417), 10 },
                    { 2, 1, "Puto2", 35, new DateTime(2024, 4, 8, 20, 39, 11, 511, DateTimeKind.Local).AddTicks(8451), 20 },
                    { 3, 1, "Puto3", 40, new DateTime(2024, 4, 8, 20, 39, 11, 511, DateTimeKind.Local).AddTicks(8463), 10 },
                    { 4, 1, "Puto4", 20, new DateTime(2024, 4, 8, 20, 39, 11, 511, DateTimeKind.Local).AddTicks(8475), 80 },
                    { 5, 1, "Puto5", 10, new DateTime(2024, 4, 8, 20, 39, 11, 511, DateTimeKind.Local).AddTicks(8486), 56 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 5);
        }
    }
}
