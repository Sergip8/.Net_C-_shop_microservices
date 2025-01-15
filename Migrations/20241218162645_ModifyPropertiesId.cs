using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPropertiesId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "IsPrincipal", "PropertyName", "PropertyOrder", "PropertyTypeId" },
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

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "PropertyId");

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "Id", "IsPrincipal", "PropertyName", "PropertyOrder", "PropertyTypeId" },
                values: new object[,]
                {
                    { 1, 0, false, "Memoria Interna", 1, 1 },
                    { 2, 0, false, "Memoria RAM", 2, 1 },
                    { 3, 0, false, "Modelo del Procesador", 2, 3 },
                    { 4, 0, false, "Frecuencia", 2, 3 },
                    { 5, 0, false, "Tipo de pantalla", 3, 1 },
                    { 6, 0, false, "Resolucion", 4, 1 },
                    { 7, 0, false, "Tamaño bateria", 3, 3 },
                    { 8, 0, false, "Bateria", 4, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_Properties_PropertyId",
                table: "PropertyValues",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
