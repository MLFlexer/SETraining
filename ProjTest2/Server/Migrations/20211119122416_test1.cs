using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AvgRating",
                table: "Content",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "Content",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LearnerId",
                table: "Content",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModeratorId",
                table: "Content",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RawVideo",
                table: "Content",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextBody",
                table: "Content",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RawImage = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Learner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Learner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moderator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguage",
                columns: table => new
                {
                    Language = table.Column<string>(type: "text", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguage", x => x.Language);
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguage_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoryEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    LearnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEntry_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryEntry_Learner_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    LearnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Content_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Learner_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_LearnerId",
                table: "Content",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_ModeratorId",
                table: "Content",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntry_ContentId",
                table: "HistoryEntry",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntry_LearnerId",
                table: "HistoryEntry",
                column: "LearnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguage_ContentId",
                table: "ProgrammingLanguage",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ContentId",
                table: "Rating",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_LearnerId",
                table: "Rating",
                column: "LearnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Learner_LearnerId",
                table: "Content",
                column: "LearnerId",
                principalTable: "Learner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Content_Moderator_ModeratorId",
                table: "Content",
                column: "ModeratorId",
                principalTable: "Moderator",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Content_Learner_LearnerId",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_Moderator_ModeratorId",
                table: "Content");

            migrationBuilder.DropTable(
                name: "HistoryEntry");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Moderator");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguage");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Learner");

            migrationBuilder.DropIndex(
                name: "IX_Content_LearnerId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_ModeratorId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "AvgRating",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "LearnerId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "RawVideo",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "TextBody",
                table: "Content");
        }
    }
}
