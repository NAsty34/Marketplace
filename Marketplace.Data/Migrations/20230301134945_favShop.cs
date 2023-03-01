using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class favShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteShop_Users_UserId",
                table: "FavoriteShop");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteShop_UserId",
                table: "FavoriteShop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FavoriteShop_UserId",
                table: "FavoriteShop",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteShop_Users_UserId",
                table: "FavoriteShop",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
