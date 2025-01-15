using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPropertyValues");

            migrationBuilder.DropTable(
                name: "ProductsImages");

            migrationBuilder.CreateTable(
                name: "ProductPropertyValue",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PropertyValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPropertyValue", x => new { x.ProductId, x.PropertyValueId });
                    table.ForeignKey(
                        name: "FK_ProductPropertyValue_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPropertyValue_PropertyValues_PropertyValueId",
                        column: x => x.PropertyValueId,
                        principalTable: "PropertyValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductPropertyValue",
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

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyValue_PropertyValueId",
                table: "ProductPropertyValue",
                column: "PropertyValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPropertyValue");

            migrationBuilder.CreateTable(
                name: "ProductPropertyValues",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PropertyValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPropertyValues", x => new { x.ProductId, x.PropertyValueId });
                    table.ForeignKey(
                        name: "FK_ProductPropertyValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPropertyValues_PropertyValues_PropertyValueId",
                        column: x => x.PropertyValueId,
                        principalTable: "PropertyValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsImages",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsImages", x => new { x.ProductId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ProductsImages_ProductImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ProductImages",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ProductPropertyValues_PropertyValueId",
                table: "ProductPropertyValues",
                column: "PropertyValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsImages_ImageId",
                table: "ProductsImages",
                column: "ImageId");
        }
    }
}
