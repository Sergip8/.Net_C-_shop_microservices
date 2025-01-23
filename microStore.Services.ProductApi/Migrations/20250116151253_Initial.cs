using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryLevel = table.Column<int>(type: "int", nullable: false),
                    CategoryParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Current_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Previous_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPrincipal = table.Column<bool>(type: "bit", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false),
                    PropertyOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyValueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyValues_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "LG" },
                    { 3, "Sony" },
                    { 4, "Panasonic" },
                    { 5, "Lenovo" },
                    { 6, "HP" },
                    { 7, "Dell" },
                    { 8, "Bose" },
                    { 9, "JBL" },
                    { 10, "Apple" },
                    { 11, "Xiaomi" },
                    { 12, "Whirlpool" },
                    { 13, "Daikin" },
                    { 14, "Samsung Appliances" },
                    { 15, "IKEA" },
                    { 16, "Ashley Furniture" },
                    { 17, "Bridgestone" },
                    { 18, "Goodyear" },
                    { 19, "Michelin" },
                    { 20, "Bosch" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryLevel", "CategoryName", "CategoryParentId" },
                values: new object[,]
                {
                    { 1, 0, "Tecnologia", 0 },
                    { 2, 1, "TV, Audio y Video", 1 },
                    { 3, 2, "Televisores", 2 },
                    { 4, 2, "Accesorios TV", 2 },
                    { 5, 1, "Computadores y Accesorios", 1 },
                    { 6, 2, "Computadores Portátiles", 5 },
                    { 7, 1, "Audio y Video", 1 },
                    { 8, 2, "Parlantes", 7 },
                    { 9, 1, "Celulares", 1 },
                    { 10, 2, "Smartphones", 9 },
                    { 11, 0, "Electrodomésticos", 0 },
                    { 12, 1, "Climatización", 11 },
                    { 13, 2, "Aires Acondicionados", 12 },
                    { 14, 1, "Cocina", 11 },
                    { 15, 2, "Hornos", 14 },
                    { 16, 0, "Muebles", 0 },
                    { 17, 1, "Muebles de Dormitorio", 16 },
                    { 18, 2, "Armarios y Closets", 17 },
                    { 19, 2, "Camas y Basecamas", 17 },
                    { 20, 0, "Automóvil", 0 },
                    { 21, 1, "Llantas", 20 },
                    { 22, 2, "Llantas Carro", 21 },
                    { 23, 1, "Accesorios Carro", 20 },
                    { 24, 2, "Batería de Auto", 23 }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "PropertyTypeDescription", "PropertyTypeName", "PropertyTypeOrder" },
                values: new object[,]
                {
                    { 1, "Caracteristicas mas destacables de los productos", "Caracteristicas Principales", 1 },
                    { 2, "Funciones principales de los productos", "Funciones", 2 },
                    { 3, "Componentes que componen el producto", "Componentes", 3 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Current_price", "Description", "IsAvailable", "Name", "Previous_price" },
                values: new object[,]
                {
                    { 1, 1, 2500000m, "Televisor Samsung de 55 pulgadas con resolución 4K y tecnología HDR.", true, "Smart TV Samsung 55\"", 3000000m },
                    { 2, 2, 4500000m, "Televisor LG OLED de 65 pulgadas con tecnología AI ThinQ.", true, "Televisor LG OLED 65\"", 5000000m },
                    { 3, 3, 100000m, "Soporte universal para televisores de hasta 55 pulgadas, fácil instalación.", true, "Soporte para TV hasta 55\"", 150000m },
                    { 4, 4, 50000m, "Cable HDMI 2.1 de 3 metros, compatible con resolución 4K y 8K.", true, "Cable HDMI 4K 3 metros", 80000m },
                    { 5, 5, 3200000m, "Portátil Lenovo ThinkPad con procesador Intel Core i5 y 8GB de RAM.", true, "Portátil Lenovo ThinkPad", 3500000m },
                    { 6, 6, 2800000m, "Portátil HP Pavilion con pantalla Full HD y disco SSD de 256GB.", true, "Portátil HP Pavilion", 3200000m },
                    { 7, 8, 600000m, "Parlante portátil Bose con conexión Bluetooth y sonido premium.", true, "Bocina Bose SoundLink", 700000m },
                    { 8, 9, 450000m, "Parlante portátil JBL con batería de larga duración y diseño resistente al agua.", true, "Parlante JBL Flip 5", 500000m },
                    { 9, 10, 5500000m, "Smartphone Apple iPhone 14 Pro con cámara de 48 MP y 256GB de almacenamiento.", true, "iPhone 14 Pro", 6000000m },
                    { 10, 11, 1200000m, "Smartphone Xiaomi con pantalla AMOLED y procesador Snapdragon 685.", true, "Xiaomi Redmi Note 12", 1500000m },
                    { 11, 14, 1800000m, "Aire acondicionado Samsung de 12000 BTU, ideal para habitaciones medianas.", true, "Aire Acondicionado Samsung", 2000000m },
                    { 12, 13, 2400000m, "Aire acondicionado Daikin con tecnología inverter y bajo consumo energético.", true, "Aire Acondicionado Daikin", 2800000m },
                    { 13, 12, 350000m, "Horno microondas Whirlpool de 20 litros con función grill.", true, "Horno Microondas Whirlpool", 450000m },
                    { 14, 14, 400000m, "Horno eléctrico Oster de 25 litros, ideal para hornear y tostar.", true, "Horno Eléctrico Oster", 500000m },
                    { 15, 15, 1500000m, "Armario IKEA Pax modular con diseño minimalista y amplio espacio de almacenamiento.", true, "Armario IKEA Pax", 1800000m },
                    { 16, 16, 2000000m, "Closet Ashley Furniture con puertas corredizas y diseño elegante.", true, "Closet Ashley Furniture", 2300000m },
                    { 17, 16, 800000m, "Basecama king size con estructura resistente y diseño moderno.", true, "Basecama King Size", 1000000m },
                    { 18, 15, 1200000m, "Cama doble de madera maciza con acabado natural.", true, "Cama Doble Madera", 1400000m },
                    { 19, 17, 350000m, "Llanta Bridgestone para automóvil, tamaño 16 pulgadas.", true, "Llanta Bridgestone 16\"", 400000m },
                    { 20, 18, 400000m, "Llanta Goodyear para SUV, tamaño 17 pulgadas.", true, "Llanta Goodyear 17\"", 450000m },
                    { 21, 20, 500000m, "Batería para automóvil Bosch con capacidad de 12V y 70Ah.", true, "Batería Bosch 12V", 600000m },
                    { 22, 19, 450000m, "Batería de auto Energizer con alto rendimiento y larga duración.", true, "Batería de Auto Energizer", 550000m }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "IsPrincipal", "PropertyName", "PropertyOrder", "PropertyTypeId" },
                values: new object[,]
                {
                    { 1, false, "Tamaño de Pantalla", 1, 1 },
                    { 2, false, "Resolución", 2, 1 },
                    { 3, false, "Tipo de Pantalla", 3, 1 },
                    { 4, false, "HDMI", 1, 2 },
                    { 5, false, "Conectividad Bluetooth", 2, 2 },
                    { 6, false, "HDR", 3, 2 },
                    { 7, false, "Control Remoto", 1, 3 },
                    { 8, false, "Puertos HDMI", 2, 3 },
                    { 9, false, "Memoria RAM", 4, 1 },
                    { 10, false, "Procesador", 5, 1 },
                    { 11, false, "Sistema Operativo", 6, 1 },
                    { 12, false, "Duración de Batería", 4, 2 },
                    { 13, false, "Conectividad Wi-Fi", 5, 2 },
                    { 14, false, "Puertos USB", 3, 3 },
                    { 15, false, "Capacidad de Almacenamiento", 7, 1 },
                    { 16, false, "Cámara", 6, 2 },
                    { 17, false, "Carga Rápida", 7, 2 },
                    { 18, false, "Tecnología Inverter", 8, 2 },
                    { 19, false, "Eficiencia Energética", 8, 1 },
                    { 20, false, "Capacidad", 9, 1 },
                    { 21, false, "Programas de Cocción", 9, 2 },
                    { 22, false, "Material de la Cava", 4, 3 },
                    { 23, false, "Potencia de Salida", 10, 1 },
                    { 24, false, "Impermeable", 10, 2 },
                    { 25, false, "Dimensiones", 11, 1 },
                    { 26, false, "Material", 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 1, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 1, 15 },
                    { 1, 16 },
                    { 1, 17 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 },
                    { 1, 21 },
                    { 1, 22 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 9 },
                    { 4, 10 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 11 },
                    { 5, 12 },
                    { 6, 7 },
                    { 6, 8 },
                    { 6, 13 },
                    { 6, 14 },
                    { 7, 9 },
                    { 7, 10 },
                    { 7, 15 },
                    { 7, 16 },
                    { 7, 17 },
                    { 7, 18 },
                    { 8, 11 },
                    { 8, 12 },
                    { 9, 13 },
                    { 9, 14 },
                    { 9, 19 },
                    { 9, 20 },
                    { 10, 15 },
                    { 10, 16 },
                    { 10, 21 },
                    { 10, 22 },
                    { 11, 17 },
                    { 11, 18 },
                    { 12, 19 },
                    { 12, 20 },
                    { 13, 21 },
                    { 13, 22 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageLabel", "ImageUrl", "ProductId" },
                values: new object[,]
                {
                    { 1, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 1 },
                    { 2, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 2 },
                    { 3, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 3 },
                    { 4, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 4 },
                    { 5, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 5 },
                    { 6, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 6 },
                    { 7, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 7 },
                    { 8, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 8 },
                    { 9, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 9 },
                    { 10, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 10 },
                    { 11, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 11 },
                    { 12, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 12 },
                    { 13, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 13 },
                    { 14, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 14 },
                    { 15, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 15 },
                    { 16, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 16 },
                    { 17, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 17 },
                    { 18, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 18 },
                    { 19, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 19 },
                    { 20, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 20 },
                    { 21, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 21 },
                    { 22, "image1", "https://www.alkosto.com/medias/8806091857859-001-750Wx750H?context=bWFzdGVyfGltYWdlc3wzODUwNnxpbWFnZS93ZWJwfGFEQXdMMmcwWWk4eE5EYzBOVFF6TXpNME1UazRNaTg0T0RBMk1Ea3hPRFUzT0RVNVh6QXdNVjgzTlRCWGVEYzFNRWd8NTU0MDIwYmI1YTZkZjc0MmIyYWU0YmZjMzIyNjM0N2VmZWRjNDMxYTJlYThlNWQyMmExMWI4OTFkOWY4ZTY0OA", 22 }
                });

            migrationBuilder.InsertData(
                table: "PropertyValues",
                columns: new[] { "Id", "PropertyId", "PropertyValueName" },
                values: new object[,]
                {
                    { 1, 1, "32 pulgadas" },
                    { 2, 1, "43 pulgadas" },
                    { 3, 1, "55 pulgadas" },
                    { 4, 1, "65 pulgadas" },
                    { 5, 2, "HD" },
                    { 6, 2, "Full HD" },
                    { 7, 2, "4K Ultra HD" },
                    { 8, 2, "8K Ultra HD" },
                    { 9, 3, "LED" },
                    { 10, 3, "OLED" },
                    { 11, 3, "QLED" },
                    { 12, 3, "IPS" },
                    { 13, 8, "2 Puertos" },
                    { 14, 8, "3 Puertos" },
                    { 15, 8, "4 Puertos" },
                    { 16, 9, "4GB" },
                    { 17, 9, "8GB" },
                    { 18, 9, "16GB" },
                    { 19, 9, "32GB" },
                    { 20, 10, "Intel Core i3" },
                    { 21, 10, "Intel Core i5" },
                    { 22, 10, "Intel Core i7" },
                    { 23, 10, "Ryzen 5" },
                    { 24, 15, "512GB SSD" },
                    { 25, 15, "1TB HDD" },
                    { 26, 15, "256GB SSD" },
                    { 27, 13, "Wi-Fi 5" },
                    { 28, 13, "Wi-Fi 6" },
                    { 29, 12, "10 horas" },
                    { 30, 12, "15 horas" },
                    { 31, 19, "Eficiencia A" },
                    { 32, 19, "Eficiencia A+" },
                    { 33, 20, "5000 BTU" },
                    { 34, 20, "12000 BTU" },
                    { 35, 23, "2000W" },
                    { 36, 23, "5000W" },
                    { 37, 26, "Madera" },
                    { 38, 26, "Metal" },
                    { 39, 26, "Plástico" }
                });

            migrationBuilder.InsertData(
                table: "ProductPropertyValue",
                columns: new[] { "ProductId", "PropertyValueId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 7 },
                    { 1, 9 },
                    { 2, 4 },
                    { 2, 7 },
                    { 2, 10 },
                    { 3, 3 },
                    { 3, 38 },
                    { 4, 7 },
                    { 4, 39 },
                    { 5, 17 },
                    { 5, 21 },
                    { 5, 26 },
                    { 6, 18 },
                    { 6, 23 },
                    { 6, 24 },
                    { 7, 27 },
                    { 7, 29 },
                    { 8, 27 },
                    { 8, 30 },
                    { 9, 7 },
                    { 9, 19 },
                    { 10, 6 },
                    { 10, 19 },
                    { 11, 32 },
                    { 11, 34 },
                    { 12, 31 },
                    { 12, 34 },
                    { 13, 35 },
                    { 13, 37 },
                    { 14, 35 },
                    { 14, 38 },
                    { 15, 37 },
                    { 16, 37 },
                    { 17, 37 },
                    { 18, 37 },
                    { 19, 38 },
                    { 20, 38 },
                    { 21, 35 },
                    { 22, 36 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyValue_PropertyValueId",
                table: "ProductPropertyValue",
                column: "PropertyValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValues_PropertyId",
                table: "PropertyValues",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductPropertyValue");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PropertyValues");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyTypes");
        }
    }
}
