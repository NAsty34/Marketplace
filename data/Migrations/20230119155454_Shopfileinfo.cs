using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class Shopfileinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Shop");

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Shop",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password" },
                values: new object[] { new DateTime(2023, 1, 19, 18, 54, 53, 679, DateTimeKind.Local).AddTicks(5635), "$2a$11$kFbllcprKo2YT2IjopdnEOttIjbZs3XEFg28xmrtI2gx1i3fl44DC" });

            migrationBuilder.CreateIndex(
                name: "IX_Shop_LogoId",
                table: "Shop",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_FileInfos_LogoId",
                table: "Shop",
                column: "LogoId",
                principalTable: "FileInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_FileInfos_LogoId",
                table: "Shop");

            migrationBuilder.DropIndex(
                name: "IX_Shop_LogoId",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Shop");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Shop",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "Password" },
                values: new object[] { new DateTime(2023, 1, 19, 17, 32, 19, 115, DateTimeKind.Local).AddTicks(8055), "$2a$11$GC2BGQZTWh5JGaSpa2LgCe8kH5mGghKQgG2vVAFboWnR0h8ZLsgE." });
        }
    }
}
