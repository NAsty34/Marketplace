using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class updateSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6c27ad12-5b8e-44b6-b3dc-175dadbaec44"));

            migrationBuilder.DropColumn(
                name: "description",
                table: "Types");

            migrationBuilder.AddColumn<string>(
                name: "discription",
                table: "Types",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "commision",
                table: "ShopPayments",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("38c8dd14-c49f-4387-adfc-ffa8c87f02b0"), new DateTime(2023, 2, 6, 14, 28, 13, 431, DateTimeKind.Local).AddTicks(7655), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$dRj.Wdpth9cbBXdF43kWve4JpAT7qPBZLMhtFtczRtE7BcZoTJ162", "Admin", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("38c8dd14-c49f-4387-adfc-ffa8c87f02b0"));

            migrationBuilder.DropColumn(
                name: "discription",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "commision",
                table: "ShopPayments");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Types",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatorId", "DeletedDate", "DeletorId", "EditDate", "EditorId", "Email", "EmailCode", "EmailIsVerified", "IsActive", "IsDeleted", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { new Guid("6c27ad12-5b8e-44b6-b3dc-175dadbaec44"), new DateTime(2023, 2, 2, 15, 26, 53, 268, DateTimeKind.Local).AddTicks(7061), null, null, null, null, null, "admin@gmail.com", null, true, true, false, "Admin", "$2a$11$DeJMEufeuS8e8aGGQB6KcOwrldKZN6yaxKC.MvS8Vykqa5Xc.UlC.", "Admin", "Admin", "Admin" });
        }
    }
}
