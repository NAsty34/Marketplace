using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class flex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76e4e940-ef65-4ba4-b3ed-2755fd46dd46"));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Types",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Commission",
                table: "PaymentMethods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Free",
                table: "DeliveryTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "parentid",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("5c17e05a-94ef-43c6-891b-31cd1f5086c4"), new DateTime(2023, 1, 27, 15, 48, 41, 943, DateTimeKind.Local).AddTicks(9209), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$wJFVENTEVEVofqSO1bizEukciKUjSC6jAwsHI0rkXNOKT47Y/bgxa", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5c17e05a-94ef-43c6-891b-31cd1f5086c4"));

            migrationBuilder.DropColumn(
                name: "description",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Commission",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "Free",
                table: "DeliveryTypes");

            migrationBuilder.DropColumn(
                name: "parentid",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("76e4e940-ef65-4ba4-b3ed-2755fd46dd46"), new DateTime(2023, 1, 23, 16, 24, 55, 140, DateTimeKind.Local).AddTicks(4852), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$/ggBfWF4vk9L0sw3MNEFheZmiVZ6uwTUMC4I6Y5LalBIgQOjdyN4C", "Admin", "Admin", "Admin" });
        }
    }
}
