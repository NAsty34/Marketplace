using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6a199d54-f9cc-4473-84a7-6ab0ff7ea912"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("8250dcb8-7733-4aa0-99ec-2aaf106515fa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$mcTYNXuhvDMShOBRU1/lA.wx6CVO3cARfz5NbnMhAWvwlhi1XcFxu", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8250dcb8-7733-4aa0-99ec-2aaf106515fa"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("6a199d54-f9cc-4473-84a7-6ab0ff7ea912"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$MzCCYIG51eS1gRf3wUukJ.zVVDlPVV6Dgwn58EjzGNE155ruibSOC", "Admin", "Admin", "Admin" });
        }
    }
}
