using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicket.Migrations
{
    /// <inheritdoc />
    public partial class screen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScreenNumber",
                table: "Shows",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenNumber",
                table: "Shows");
        }
    }
}
