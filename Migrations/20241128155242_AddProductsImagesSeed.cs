using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProductsImagesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsImages",
                table: "ProductsImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductsImages_ProductId",
                table: "ProductsImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsImages",
                table: "ProductsImages",
                columns: new[] { "ProductId", "ImageId" });

            migrationBuilder.InsertData(
                table: "ProductsImages",
                columns: new[] { "ImageId", "ProductId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 4 },
                    { 7, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsImages_ImageId",
                table: "ProductsImages",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsImages",
                table: "ProductsImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductsImages_ImageId",
                table: "ProductsImages");

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "ProductsImages",
                keyColumns: new[] { "ImageId", "ProductId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsImages",
                table: "ProductsImages",
                columns: new[] { "ImageId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsImages_ProductId",
                table: "ProductsImages",
                column: "ProductId");
        }
    }
}
