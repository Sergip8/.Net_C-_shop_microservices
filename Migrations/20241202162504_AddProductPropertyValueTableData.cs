using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPropertyValueTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductPropertyValues",
                columns: new[] { "ProductId", "PropertyValueId" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 12 },
                    { 3, 14 },
                    { 3, 15 },
                    { 3, 19 },
                    { 3, 21 },
                    { 3, 23 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 19 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 21 });

            migrationBuilder.DeleteData(
                table: "ProductPropertyValues",
                keyColumns: new[] { "ProductId", "PropertyValueId" },
                keyValues: new object[] { 3, 23 });
        }
    }
}
