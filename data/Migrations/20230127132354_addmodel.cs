using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class addmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("778f1388-6c6c-4158-8c6f-d15b0a7f498c"));

            migrationBuilder.CreateTable(
                name: "ShopCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    shopid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopDeliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    shopid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopDeliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Paymentid = table.Column<Guid>(type: "uuid", nullable: false),
                    shopid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    shopid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("9e28bb71-4ae8-4107-aab9-854fb6b08684"), new DateTime(2023, 1, 27, 16, 23, 53, 584, DateTimeKind.Local).AddTicks(5713), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$OSJ3CaxKaHwrq1XXbi4O4.91K/cWMK0J45VpaCQ.bB1QsuXvaMhdW", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopCategories");

            migrationBuilder.DropTable(
                name: "ShopDeliveries");

            migrationBuilder.DropTable(
                name: "ShopPayments");

            migrationBuilder.DropTable(
                name: "ShopTypes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9e28bb71-4ae8-4107-aab9-854fb6b08684"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("778f1388-6c6c-4158-8c6f-d15b0a7f498c"), new DateTime(2023, 1, 27, 16, 14, 18, 746, DateTimeKind.Local).AddTicks(3095), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$DbxjhplnOYRz93cQdQ1kNe/6PdrHyxc3C2nqrIiaK6fL9YBHGTBE6", "Admin", "Admin", "Admin" });
        }
    }
}
