using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentProgrammingLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProgrammingLanguage");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "ProgrammingLanguage",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage",
                column: "Language");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguage_ContentId",
                table: "ProgrammingLanguage",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammingLanguage_Content_ContentId",
                table: "ProgrammingLanguage",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammingLanguage_Content_ContentId",
                table: "ProgrammingLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ProgrammingLanguage_ContentId",
                table: "ProgrammingLanguage");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "ProgrammingLanguage");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProgrammingLanguage",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContentProgrammingLanguage",
                columns: table => new
                {
                    ContentsId = table.Column<int>(type: "integer", nullable: false),
                    ProgrammingLanguagesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentProgrammingLanguage", x => new { x.ContentsId, x.ProgrammingLanguagesId });
                    table.ForeignKey(
                        name: "FK_ContentProgrammingLanguage_Content_ContentsId",
                        column: x => x.ContentsId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentProgrammingLanguage_ProgrammingLanguage_ProgrammingL~",
                        column: x => x.ProgrammingLanguagesId,
                        principalTable: "ProgrammingLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentProgrammingLanguage_ProgrammingLanguagesId",
                table: "ContentProgrammingLanguage",
                column: "ProgrammingLanguagesId");
        }
    }
}
