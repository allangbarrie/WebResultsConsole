using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdminConsole.Migrations
{
    /// <inheritdoc />
    public partial class Leaderboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "LeaderBoard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaderBoard_StageId",
                table: "LeaderBoard",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderBoard_Stage_StageId",
                table: "LeaderBoard",
                column: "StageId",
                principalTable: "Stage",
                principalColumn: "StageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderBoard_Stage_StageId",
                table: "LeaderBoard");

            migrationBuilder.DropIndex(
                name: "IX_LeaderBoard_StageId",
                table: "LeaderBoard");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "LeaderBoard");
        }
    }
}
