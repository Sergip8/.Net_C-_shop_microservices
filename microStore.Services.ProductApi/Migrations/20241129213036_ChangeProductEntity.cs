using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceRanges");

            migrationBuilder.AddColumn<decimal>(
                name: "Current_price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Previous_price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Current_price", "Previous_price" },
                values: new object[] { 3000000m, 3500000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "Current_price", "Previous_price" },
                values: new object[] { 3000000m, 3500000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Current_price", "Previous_price" },
                values: new object[] { 3000000m, 3500000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "Current_price", "Previous_price" },
                values: new object[] { 3000000m, 3500000m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Current_price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Previous_price",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "PriceRanges",
                columns: table => new
                {
                    PriceRangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LowPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRanges", x => x.PriceRangeId);
                });

            migrationBuilder.InsertData(
                table: "PriceRanges",
                columns: new[] { "PriceRangeId", "HighPrice", "LowPrice" },
                values: new object[] { 1, 3000000m, 2800000m });
        }
    }
}
