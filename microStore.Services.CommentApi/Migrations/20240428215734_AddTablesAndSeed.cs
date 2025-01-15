using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace microStore.Services.CommentApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentHeader",
                columns: table => new
                {
                    CommentHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OverallScore = table.Column<float>(type: "real", nullable: false),
                    QtyForStar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentHeader", x => x.CommentHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    CommentHeaderId = table.Column<int>(type: "int", nullable: false),
                    CommentUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_CommentHeader_CommentHeaderId",
                        column: x => x.CommentHeaderId,
                        principalTable: "CommentHeader",
                        principalColumn: "CommentHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CommentHeader",
                columns: new[] { "CommentHeaderId", "CommentCount", "OverallScore", "ProductId", "QtyForStar" },
                values: new object[] { 1, 7, 2f, 3, "" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "CommentHeaderId", "Content", "CreatedDate", "Score", "Title", "Votes", "CommentUserId" },
                values: new object[,]
                {
                    { 1, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0, "d3733ffc-aafe-448f-bdb5-10b18c941a15" },
                    { 2, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0, "d3733ffc-aafe-448f-bdb5-10b18c941a15" },
                    { 3, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0 , "d3733ffc-aafe-448f-bdb5-10b18c941a15"},
                    { 4, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0, "d3733ffc-aafe-448f-bdb5-10b18c941a15" },
                    { 5, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0 , "d3733ffc-aafe-448f-bdb5-10b18c941a15"},
                    { 6, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0, "d3733ffc-aafe-448f-bdb5-10b18c941a15"},
                    { 7, 1, "Una mierda pinchada en un palo", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Comment", 0, "d3733ffc-aafe-448f-bdb5-10b18c941a15" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentHeaderId",
                table: "Comments",
                column: "CommentHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CommentHeader");
        }
    }
}
