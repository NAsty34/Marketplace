using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class free : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("38c8dd14-c49f-4387-adfc-ffa8c87f02b0"));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ShopDeliveries",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("f953f95d-0fc6-4323-a893-fddcf0f37c53"), new DateTime(2023, 2, 8, 2, 42, 9, 138, DateTimeKind.Local).AddTicks(2805), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$bFYwgEYAOU8yOKPe.4ofDu50k7Xg/8lgGbnVzEXidw.BnJuusYFI2", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f953f95d-0fc6-4323-a893-fddcf0f37c53"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShopDeliveries");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("38c8dd14-c49f-4387-adfc-ffa8c87f02b0"), new DateTime(2023, 2, 6, 14, 28, 13, 431, DateTimeKind.Local).AddTicks(7655), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$dRj.Wdpth9cbBXdF43kWve4JpAT7qPBZLMhtFtczRtE7BcZoTJ162", "Admin", "Admin", "Admin" });
        }
    }
}
