using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjTest2.Server.Migrations
{
    public partial class test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Difficulty = table.Column<int>(type: "integer", nullable: true),
                    CreatorId = table.Column<int>(type: "integer", nullable: false),
                    AvgRating = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    LearnerId = table.Column<int>(type: "integer", nullable: true),
                    TextBody = table.Column<string>(type: "text", nullable: true),
                    Length = table.Column<int>(type: "integer", nullable: true),
                    RawVideo = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Content_Learner_LearnerId",
                        column: x => x.LearnerId,
                        principalTable: "Learner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Content_Moderator_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Moderator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Content_CreatorId",
                table: "Content",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_LearnerId",
                table: "Content",
                column: "LearnerId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEntry");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguage");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "Learner");

            migrationBuilder.DropTable(
                name: "Moderator");
        }
    }
}
