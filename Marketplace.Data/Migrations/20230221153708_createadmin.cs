using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class createadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email",
                    "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role",
                    "Surname"
                },
                values: new object[]
                {
                    new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa"),
                    new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null,
                    "admin@gmail.com", null, true, true, false, "Admin", BCrypt.Net.BCrypt.HashPassword("0000"),
                    "Admin", "Admin", "Admin"
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8250bcb8-7733-4aa0-99ec-2aaf106515fa")
            );
        }
    }
}

