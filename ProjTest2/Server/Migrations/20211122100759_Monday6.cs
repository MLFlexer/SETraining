using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class Monday6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Content_ContentId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Learner_LearnerId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "RawVideo",
                table: "Content");

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

            migrationBuilder.AddColumn<int>(
                name: "RawVideoId",
                table: "Content",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RawVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Video = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawVideo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_RawVideoId",
                table: "Content",
                column: "RawVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_RawVideo_RawVideoId",
                table: "Content",
                column: "RawVideoId",
                principalTable: "RawVideo",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_RawVideo_RawVideoId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Content_ContentId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Learner_LearnerId",
                table: "Rating");

            migrationBuilder.DropTable(
                name: "RawVideo");

            migrationBuilder.DropIndex(
                name: "IX_Content_RawVideoId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "RawVideoId",
                table: "Content");

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

            migrationBuilder.AddColumn<byte[]>(
                name: "RawVideo",
                table: "Content",
                type: "bytea",
                nullable: true);

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
    }
}
