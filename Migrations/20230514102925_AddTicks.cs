using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAdminConsole.Migrations
{
    /// <inheritdoc />
    public partial class AddTicks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ticks",
                table: "LeaderBoard",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ticks",
                table: "LeaderBoard");
        }
    }
}
