using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wmbaApp.Data.WMMigrations
{
    /// <inheritdoc />
    public partial class addplln : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayersLineUp_playersLineUpId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "playersLineUpId",
                table: "Players",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayersLineUp_playersLineUpId",
                table: "Players",
                column: "playersLineUpId",
                principalTable: "PlayersLineUp",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_PlayersLineUp_playersLineUpId",
                table: "Players");

            migrationBuilder.AlterColumn<int>(
                name: "playersLineUpId",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_PlayersLineUp_playersLineUpId",
                table: "Players",
                column: "playersLineUpId",
                principalTable: "PlayersLineUp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
