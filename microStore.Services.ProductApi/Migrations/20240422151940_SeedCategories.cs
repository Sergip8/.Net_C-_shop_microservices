using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCategories_Categories_CategoriescategoryId",
                table: "ProductsCategories");

            migrationBuilder.RenameColumn(
                name: "CategoriescategoryId",
                table: "ProductsCategories",
                newName: "CategoriesCategoryId");

            migrationBuilder.RenameColumn(
                name: "categoryName",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryParentId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryLevel", "CategoryName", "CategoryParentId" },
                values: new object[,]
                {
                    { 1, 0, "Cat 1", 0 },
                    { 2, 1, "Cat 2", 1 },
                    { 3, 2, "Cat 3", 2 },
                    { 4, 1, "Cat 4", 1 },
                    { 5, 2, "Cat 5", 4 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCategories_Categories_CategoriesCategoryId",
                table: "ProductsCategories",
                column: "CategoriesCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCategories_Categories_CategoriesCategoryId",
                table: "ProductsCategories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "CategoryParentId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoriesCategoryId",
                table: "ProductsCategories",
                newName: "CategoriescategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "categoryName");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCategories_Categories_CategoriescategoryId",
                table: "ProductsCategories",
                column: "CategoriescategoryId",
                principalTable: "Categories",
                principalColumn: "categoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
