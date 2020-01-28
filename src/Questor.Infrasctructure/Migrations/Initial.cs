using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Questor.Infrasctructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: false),
                    EngineTypeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchResultItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    SearchResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchResultItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchResultItems_SearchResults_SearchResultId",
                        column: x => x.SearchResultId,
                        principalTable: "SearchResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchResultItems_SearchResultId",
                table: "SearchResultItems",
                column: "SearchResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchResultItems");

            migrationBuilder.DropTable(
                name: "SearchResults");
        }
    }
}
