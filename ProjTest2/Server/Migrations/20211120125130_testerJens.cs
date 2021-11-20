using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class testerJens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Moderator_CreatorId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Content_ContentId",
                table: "HistoryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Learner_LearnerId",
                table: "HistoryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Content_ContentId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Learner_LearnerId",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "Learner",
                newName: "Level");

            migrationBuilder.AlterColumn<int>(
                name: "LearnerId",
                table: "Rating",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ContentId",
                table: "Rating",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Content",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<float>(
                name: "AvgRating",
                table: "Content",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Moderator_CreatorId",
                table: "Content",
                column: "CreatorId",
                principalTable: "Moderator",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Content_ContentId",
                table: "Rating",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Learner_LearnerId",
                table: "Rating",
                column: "LearnerId",
                principalTable: "Learner",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Moderator_CreatorId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Content_ContentId",
                table: "HistoryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEntry_Learner_LearnerId",
                table: "HistoryEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Content_ContentId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Learner_LearnerId",
                table: "Rating");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Learner",
                newName: "level");

            migrationBuilder.AlterColumn<int>(
                name: "LearnerId",
                table: "Rating",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContentId",
                table: "Rating",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Content",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AvgRating",
                table: "Content",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Moderator_CreatorId",
                table: "Content",
                column: "CreatorId",
                principalTable: "Moderator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Content_ContentId",
                table: "Rating",
                column: "ContentId",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Learner_LearnerId",
                table: "Rating",
                column: "LearnerId",
                principalTable: "Learner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
