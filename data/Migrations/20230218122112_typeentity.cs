using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class typeentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a5846b6d-deca-4292-b412-5837f1a8dc19"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("651646d2-abcf-4ff7-be72-2d5f2b9c93c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$UlSC.XXt2TBZj9JcUeX3yeTVH065xDyKMpBjZ8gR.ffMBH2nt3RVK", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("651646d2-abcf-4ff7-be72-2d5f2b9c93c2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("a5846b6d-deca-4292-b412-5837f1a8dc19"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$bx75JlZmtuy1yWuBiM79Fubw4rz33t5az89Ys8KE3MjQXKwD5IF1e", "Admin", "Admin", "Admin" });
        }
    }
}
