using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class requerd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28d6c474-5754-4326-93db-3f86498e5f00"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("a7a6a412-1d2e-4406-bd2f-99416333ca2d"), new DateTime(2023, 2, 13, 17, 22, 38, 169, DateTimeKind.Local).AddTicks(5737), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$UD0oLCWrwYI.n/nGDAPjLeSv7GvctWqWJ2aUPLSk1fjf/yf4EmIN6", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a7a6a412-1d2e-4406-bd2f-99416333ca2d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("28d6c474-5754-4326-93db-3f86498e5f00"), new DateTime(2023, 2, 8, 3, 5, 54, 913, DateTimeKind.Local).AddTicks(32), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$7HSGeeowHoGGv3u7I5UeO..xYcEyPCT5ElUBAMPREyFjKfkO8oL2q", "Admin", "Admin", "Admin" });
        }
    }
}
