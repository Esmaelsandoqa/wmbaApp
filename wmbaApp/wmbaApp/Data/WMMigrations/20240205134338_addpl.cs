using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wmbaApp.Data.WMMigrations
{
    /// <inheritdoc />
    public partial class addpl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayersLineUp_Games_GameId",
                table: "PlayersLineUp");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersLineUp_Players_PlayerId",
                table: "PlayersLineUp");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersLineUp_Teams_TeamId",
                table: "PlayersLineUp");

            migrationBuilder.DropIndex(
                name: "IX_PlayersLineUp_PlayerId",
                table: "PlayersLineUp");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayersLineUp");

            migrationBuilder.RenameColumn(
                name: "PlayersLineUpName",
                table: "PlayersLineUp",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "PlayersLineUp",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "PlayersLineUp",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "playersLineUpId",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_playersLineUpId",
                table: "Players",
                column: "playersLineUpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayersLineUp_playersLineUpId",
                table: "Players",
                column: "playersLineUpId",
                principalTable: "PlayersLineUp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersLineUp_Games_GameId",
                table: "PlayersLineUp",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersLineUp_Teams_TeamId",
                table: "PlayersLineUp",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayersLineUp_playersLineUpId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersLineUp_Games_GameId",
                table: "PlayersLineUp");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayersLineUp_Teams_TeamId",
                table: "PlayersLineUp");

            migrationBuilder.DropIndex(
                name: "IX_Players_playersLineUpId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "playersLineUpId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PlayersLineUp",
                newName: "PlayersLineUpName");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "PlayersLineUp",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "PlayersLineUp",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayersLineUp",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayersLineUp_PlayerId",
                table: "PlayersLineUp",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersLineUp_Games_GameId",
                table: "PlayersLineUp",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersLineUp_Players_PlayerId",
                table: "PlayersLineUp",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayersLineUp_Teams_TeamId",
                table: "PlayersLineUp",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "ID");
        }
    }
}
