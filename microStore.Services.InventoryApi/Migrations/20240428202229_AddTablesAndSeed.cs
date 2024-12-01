using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.InventoryApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RetailPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "ProductId", "Quantity", "RetailPrice", "VendorId" },
                values: new object[,]
                {
                    { 1, 1, 20, 100000m, 1 },
                    { 2, 2, 453, 100000m, 2 },
                    { 3, 3, 20, 100000m, 3 },
                    { 4, 4, 45, 100000m, 4 },

                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "VendorName" },
                values: new object[,]
                {
                    { 1, "Vendor1" },
                    { 2, "Vendor1" },
                    { 3, "Vendor2" },
                    { 4, "Vendor1" },

                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
