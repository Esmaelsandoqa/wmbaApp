using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wmbaApp.Data.WMMigrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileProperty");

            migrationBuilder.DropTable(
                name: "PlayerPhotos");

            migrationBuilder.DropTable(
                name: "PlayerThumbnails");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PlyrDOB",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "competitorTeam",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "team",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PlayersLineUp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayersLineUpName = table.Column<string>(type: "TEXT", nullable: true),
                    GameId = table.Column<int>(type: "INTEGER", nullable: true),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersLineUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayersLineUp_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PlayersLineUp_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PlayersLineUp_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_PlyrJerseyNumber_TeamID",
                table: "Players",
                columns: new[] { "PlyrJerseyNumber", "TeamID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayersLineUp_GameId",
                table: "PlayersLineUp",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersLineUp_PlayerId",
                table: "PlayersLineUp",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersLineUp_TeamId",
                table: "PlayersLineUp",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersLineUp");

            migrationBuilder.DropIndex(
                name: "IX_Players_PlyrJerseyNumber_TeamID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "competitorTeam",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "team",
                table: "Games");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PlyrDOB",
                table: "Players",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "PlayerPhotos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerPhotos_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerThumbnails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerThumbnails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerThumbnails_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FileProperty",
                columns: table => new
                {
                    FilePropertyID = table.Column<int>(type: "INTEGER", nullable: false),
                    Property = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileProperty", x => x.FilePropertyID);
                    table.ForeignKey(
                        name: "FK_FileProperty_UploadedFiles_FilePropertyID",
                        column: x => x.FilePropertyID,
                        principalTable: "UploadedFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPhotos_PlayerID",
                table: "PlayerPhotos",
                column: "PlayerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerThumbnails_PlayerID",
                table: "PlayerThumbnails",
                column: "PlayerID",
                unique: true);
        }
    }
}
