using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsCategoriesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsCategories",
                table: "ProductsCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductsCategories_ProductId",
                table: "ProductsCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsCategories",
                table: "ProductsCategories",
                columns: new[] { "ProductId", "CategoryId" });

            migrationBuilder.InsertData(
                table: "ProductsCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCategories_CategoryId",
                table: "ProductsCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsCategories",
                table: "ProductsCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductsCategories_CategoryId",
                table: "ProductsCategories");

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "ProductsCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsCategories",
                table: "ProductsCategories",
                columns: new[] { "CategoryId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCategories_ProductId",
                table: "ProductsCategories",
                column: "ProductId");
        }
    }
}
