using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {






            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.PropertyTypeId);
                });




            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPrincipal = table.Column<bool>(type: "bit", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "PropertyTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValues",
                columns: table => new
                {
                    PropertyValueId = table.Column<int>(type: "int", nullable: false),
                    PropertyValueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValues", x => x.PropertyValueId);
                    table.ForeignKey(
                        name: "FK_PropertyValues_Properties_PropertyValueId",
                        column: x => x.PropertyValueId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPropertyValues_PropertyValues_PropertyValueId",
                        column: x => x.PropertyValueId,
                        principalTable: "PropertyValues",
                        principalColumn: "PropertyValueId",
                        onDelete: ReferentialAction.Cascade);
                });






            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "PropertyTypeId", "PropertyTypeDescription", "PropertyTypeName", "PropertyTypeOrder" },
                values: new object[,]
                {
                    { 1, "Caracteristicas mas destacables de los productos", "Caracteristicas Principales", 1 },
                    { 2, "Funciones principales de los productos", "Funciones", 2 },
                    { 3, "Componentes que componen el producto", "Componentes", 3 }
                });
            migrationBuilder.InsertData(
             table: "Properties",
             columns: new[] { "PropertyId", "IsPrincipal", "PropertyName", "PropertyOrder", "PropertyTypeId" },
             values: new object[,]
             {
                    { 1, false, "Memoria Interna", 1, 1 },
                    { 2, false, "Memoria RAM", 2, 1 },
                    { 3, false, "Modelo del Procesador", 2, 3 },
                    { 4, false, "Frecuencia", 2, 3 },
                    { 5, false, "Tipo de pantalla", 3, 1 },
                    { 6, false, "Resolucion", 4, 1 },
                    { 7, false, "Tamaño bateria", 3, 3 },
                    { 8, false, "Bateria", 4, 3 }
             });

            migrationBuilder.InsertData(
                table: "PropertyValues",
                columns: new[] { "PropertyValueId", "PropertyId", "PropertyValueName" },
                values: new object[,]
                {
                    { 9, 2, "12GB" },
                    { 10, 2, "16GB" },
                    { 11, 3, "Mediatek" },
                    { 12, 3, "Qualcomm" },
                    { 13, 4, "3.3 GHz" },
                    { 14, 4, "2.8 GHz" },
                    { 15, 5, "AMOLED" },
                    { 16, 5, "LED" },
                    { 18, 6, "1080 x 2400 px • FHD+" },
                    { 19, 6, "1440 x 3168 px • WQHD+" },
                    { 20, 7, "6.55\"" },
                    { 21, 7, "6.82\"" },
                    { 22, 8, "5000 mAh" },
                    { 23, 8, "4000 mAh" }
                });



            migrationBuilder.InsertData(
                table: "PropertyValues",
                columns: new[] { "PropertyValueId", "PropertyId", "PropertyValueName" },
                values: new object[,]
                {
                    { 1, 1, "512GB" },
                    { 2, 1, "256GB" },
                    { 3, 1, "128GB" },
                    { 4, 1, "64GB" },
                    { 5, 1, "1000GB" },
                    { 6, 2, "8GB" },
                    { 7, 2, "4GB" },
                    { 8, 2, "6GB" }
                });


            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyValues_PropertyValueId",
                table: "ProductPropertyValues",
                column: "PropertyValueId");


            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "PriceRanges");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductPropertyValues");

            migrationBuilder.DropTable(
                name: "ProductsCategories");

            migrationBuilder.DropTable(
                name: "PropertyValues");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyTypes");
        }
    }
}
