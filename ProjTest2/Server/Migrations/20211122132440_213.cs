using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class _213 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentProgrammingLanguage_ProgrammingLanguage_ProgrammingL~",
                table: "ContentProgrammingLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentProgrammingLanguage",
                table: "ContentProgrammingLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ContentProgrammingLanguage_ProgrammingLanguagesLanguage",
                table: "ContentProgrammingLanguage");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguagesLanguage",
                table: "ContentProgrammingLanguage");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProgrammingLanguage",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammingLanguagesId",
                table: "ContentProgrammingLanguage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentProgrammingLanguage",
                table: "ContentProgrammingLanguage",
                columns: new[] { "ContentsId", "ProgrammingLanguagesId" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentProgrammingLanguage_ProgrammingLanguagesId",
                table: "ContentProgrammingLanguage",
                column: "ProgrammingLanguagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentProgrammingLanguage_ProgrammingLanguage_ProgrammingL~",
                table: "ContentProgrammingLanguage",
                column: "ProgrammingLanguagesId",
                principalTable: "ProgrammingLanguage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentProgrammingLanguage_ProgrammingLanguage_ProgrammingL~",
                table: "ContentProgrammingLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContentProgrammingLanguage",
                table: "ContentProgrammingLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ContentProgrammingLanguage_ProgrammingLanguagesId",
                table: "ContentProgrammingLanguage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProgrammingLanguage");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguagesId",
                table: "ContentProgrammingLanguage");

            migrationBuilder.AddColumn<string>(
                name: "ProgrammingLanguagesLanguage",
                table: "ContentProgrammingLanguage",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgrammingLanguage",
                table: "ProgrammingLanguage",
                column: "Language");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContentProgrammingLanguage",
                table: "ContentProgrammingLanguage",
                columns: new[] { "ContentsId", "ProgrammingLanguagesLanguage" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentProgrammingLanguage_ProgrammingLanguagesLanguage",
                table: "ContentProgrammingLanguage",
                column: "ProgrammingLanguagesLanguage");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentProgrammingLanguage_ProgrammingLanguage_ProgrammingL~",
                table: "ContentProgrammingLanguage",
                column: "ProgrammingLanguagesLanguage",
                principalTable: "ProgrammingLanguage",
                principalColumn: "Language",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
