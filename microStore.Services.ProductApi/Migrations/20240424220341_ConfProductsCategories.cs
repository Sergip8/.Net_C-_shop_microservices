using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class ConfProductsCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCategories_Categories_CategoriesCategoryId",
                table: "ProductsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCategories_Products_ProductsProductId",
                table: "ProductsCategories");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "ProductsCategories",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CategoriesCategoryId",
                table: "ProductsCategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsCategories_ProductsProductId",
                table: "ProductsCategories",
                newName: "IX_ProductsCategories_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCategories_Categories_CategoryId",
                table: "ProductsCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCategories_Products_ProductId",
                table: "ProductsCategories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCategories_Categories_CategoryId",
                table: "ProductsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCategories_Products_ProductId",
                table: "ProductsCategories");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductsCategories",
                newName: "ProductsProductId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ProductsCategories",
                newName: "CategoriesCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsCategories_ProductId",
                table: "ProductsCategories",
                newName: "IX_ProductsCategories_ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCategories_Categories_CategoriesCategoryId",
                table: "ProductsCategories",
                column: "CategoriesCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCategories_Products_ProductsProductId",
                table: "ProductsCategories",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
