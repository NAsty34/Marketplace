using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class newfree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f953f95d-0fc6-4323-a893-fddcf0f37c53"));

            migrationBuilder.AddColumn<double>(
                name: "MinPrice",
                table: "Shop",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("28d6c474-5754-4326-93db-3f86498e5f00"), new DateTime(2023, 2, 8, 3, 5, 54, 913, DateTimeKind.Local).AddTicks(32), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$7HSGeeowHoGGv3u7I5UeO..xYcEyPCT5ElUBAMPREyFjKfkO8oL2q", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28d6c474-5754-4326-93db-3f86498e5f00"));

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Shop");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("f953f95d-0fc6-4323-a893-fddcf0f37c53"), new DateTime(2023, 2, 8, 2, 42, 9, 138, DateTimeKind.Local).AddTicks(2805), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$bFYwgEYAOU8yOKPe.4ofDu50k7Xg/8lgGbnVzEXidw.BnJuusYFI2", "Admin", "Admin", "Admin" });
        }
    }
}
