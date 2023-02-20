using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class renamefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("651646d2-abcf-4ff7-be72-2d5f2b9c93c2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("6a199d54-f9cc-4473-84a7-6ab0ff7ea912"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$MzCCYIG51eS1gRf3wUukJ.zVVDlPVV6Dgwn58EjzGNE155ruibSOC", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6a199d54-f9cc-4473-84a7-6ab0ff7ea912"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("651646d2-abcf-4ff7-be72-2d5f2b9c93c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$UlSC.XXt2TBZj9JcUeX3yeTVH065xDyKMpBjZ8gR.ffMBH2nt3RVK", "Admin", "Admin", "Admin" });
        }
    }
}
