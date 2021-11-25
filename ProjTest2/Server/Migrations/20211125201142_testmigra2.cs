using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class testmigra2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_RawVideo_RawVideoId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "RawVideoId",
                table: "Content",
                newName: "RawDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_RawVideoId",
                table: "Content",
                newName: "IX_Content_RawDataId");

            migrationBuilder.AlterColumn<string>(
                name: "Difficulty",
                table: "Content",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_RawVideo_RawDataId",
                table: "Content",
                column: "RawDataId",
                principalTable: "RawVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_RawVideo_RawDataId",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "RawDataId",
                table: "Content",
                newName: "RawVideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Content_RawDataId",
                table: "Content",
                newName: "IX_Content_RawVideoId");

            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "Content",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_RawVideo_RawVideoId",
                table: "Content",
                column: "RawVideoId",
                principalTable: "RawVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
