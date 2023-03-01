using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class favShop3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopEntityId",
                table: "FavoriteShop",
                newName: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteShop_UserId",
                table: "FavoriteShop",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteShop_Shop_ShopId",
                table: "FavoriteShop",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteShop_Users_UserId",
                table: "FavoriteShop",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteShop_Shop_ShopId",
                table: "FavoriteShop");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteShop_Users_UserId",
                table: "FavoriteShop");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteShop_UserId",
                table: "FavoriteShop");

            migrationBuilder.RenameColumn(
                name: "ShopId",
                table: "FavoriteShop",
                newName: "ShopEntityId");
        }
    }
}
