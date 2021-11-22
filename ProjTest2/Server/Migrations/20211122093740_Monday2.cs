using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class Monday2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Content_ContentId",
                table: "HistoryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Learner_LearnerId",
                table: "HistoryEntry");

            migrationBuilder.AlterColumn<int>(
                name: "LearnerId",
                table: "HistoryEntry",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContentId",
                table: "HistoryEntry",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntry_Content_ContentId",
                table: "HistoryEntry",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntry_Learner_LearnerId",
                table: "HistoryEntry",
                column: "LearnerId",
                principalTable: "Learner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Content_ContentId",
                table: "HistoryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Learner_LearnerId",
                table: "HistoryEntry");

            migrationBuilder.AlterColumn<int>(
                name: "LearnerId",
                table: "HistoryEntry",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ContentId",
                table: "HistoryEntry",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntry_Content_ContentId",
                table: "HistoryEntry",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEntry_Learner_LearnerId",
                table: "HistoryEntry",
                column: "LearnerId",
                principalTable: "Learner",
                principalColumn: "Id");
        }
    }
}
