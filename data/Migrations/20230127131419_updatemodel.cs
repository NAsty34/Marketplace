using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5c17e05a-94ef-43c6-891b-31cd1f5086c4"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("778f1388-6c6c-4158-8c6f-d15b0a7f498c"), new DateTime(2023, 1, 27, 16, 14, 18, 746, DateTimeKind.Local).AddTicks(3095), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$DbxjhplnOYRz93cQdQ1kNe/6PdrHyxc3C2nqrIiaK6fL9YBHGTBE6", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("778f1388-6c6c-4158-8c6f-d15b0a7f498c"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("5c17e05a-94ef-43c6-891b-31cd1f5086c4"), new DateTime(2023, 1, 27, 15, 48, 41, 943, DateTimeKind.Local).AddTicks(9209), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$wJFVENTEVEVofqSO1bizEukciKUjSC6jAwsHI0rkXNOKT47Y/bgxa", "Admin", "Admin", "Admin" });
        }
    }
}
