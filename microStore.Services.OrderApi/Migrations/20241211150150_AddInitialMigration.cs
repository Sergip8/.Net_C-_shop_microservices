using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.OrderApi.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    ShippingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ShippedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.ShippingId);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.OrderHeaderId);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "ShippingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    OrderHeaderId1 = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailsId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "OrderHeaderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId1",
                        column: x => x.OrderHeaderId1,
                        principalTable: "OrderHeaders",
                        principalColumn: "OrderHeaderId");
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "Method", "PaidAt", "Status" },
                values: new object[,]
                {
                    { 1, 75.00m, 0, new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4584), 1 },
                    { 2, 75.00m, 0, new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4590), 1 }
                });

            migrationBuilder.InsertData(
                table: "Shippings",
                columns: new[] { "ShippingId", "Address", "City", "Country", "DeliveredAt", "PostalCode", "ShippedAt", "Status" },
                values: new object[,]
                {
                    { 1, "Calle 123, Bogotá", "Bogotá", "Colombia", new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4613), "110111", new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4610), 0 },
                    { 2, "Calle 123, Bogotá", "Bogotá", "Colombia", new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4616), "110111", new DateTime(2024, 12, 9, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4615), 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderHeaders",
                columns: new[] { "OrderHeaderId", "CouponCode", "Discount", "Email", "Name", "OrderTime", "OrderTotal", "PaymentId", "Phone", "ShippingId", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "20% off", 20m, "ccc@cc.com", "user1", new DateTime(2024, 12, 11, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4457), 2400.0m, 1, "513255565", 1, 3, "d3733ffc-aafe-448f-bdb5-10b18c941a15" },
                    { 2, "20% off", 20m, "ccc@cc.com", "user1", new DateTime(2024, 12, 11, 15, 1, 49, 438, DateTimeKind.Utc).AddTicks(4461), 2400.0m, 2, "513255565", 2, 3, "d3733ffc-aafe-448f-bdb5-10b18c941a15" }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailsId", "OrderHeaderId", "OrderHeaderId1", "ProductId", "ProductName", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, null, 3, "Producto A", 2, 25.00m },
                    { 2, 1, null, 2, "Producto B", 1, 25.00m },
                    { 3, 2, null, 3, "Producto A", 2, 25.00m },
                    { 4, 2, null, 2, "Producto B", 1, 25.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId1",
                table: "OrderDetails",
                column: "OrderHeaderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_PaymentId",
                table: "OrderHeaders",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ShippingId",
                table: "OrderHeaders",
                column: "ShippingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shippings");
        }
    }
}
