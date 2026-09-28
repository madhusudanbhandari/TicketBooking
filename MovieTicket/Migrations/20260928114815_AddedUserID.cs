using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicket.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cinemas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_UserId",
                table: "Cinemas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Users_UserId",
                table: "Cinemas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Users_UserId",
                table: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_UserId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cinemas");
        }
    }
}
