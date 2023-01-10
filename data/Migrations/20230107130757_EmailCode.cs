using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class EmailCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Shops_ShopId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_CreatorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_DeletorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_EditorId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Users_CreatorId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Users_DeletorId",
                table: "Shops");

            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Users_EditorId",
                table: "Shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shops",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_DeletorId",
                table: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Shops_EditorId",
                table: "Shops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_DeletorId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_EditorId",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Shops",
                newName: "Shop");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_CreatorId",
                table: "Shop",
                newName: "IX_Shop_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ShopId",
                table: "Feedback",
                newName: "IX_Feedback_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_CreatorId",
                table: "Feedback",
                newName: "IX_Feedback_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailCode",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailIsVerified",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shop",
                table: "Shop",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DataRegistration", "Email", "EmailCode", "EmailIsVerified", "Name", "Password", "Patronymic", "Role", "Surname" },
                values: new object[] { 1, new DateTime(2023, 1, 7, 16, 7, 56, 655, DateTimeKind.Local).AddTicks(5892), "admin@gmail.com", null, false, "Admin", "0000", "Admin", "Admin", "Admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Shop_ShopId",
                table: "Feedback",
                column: "ShopId",
                principalTable: "Shop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Users_CreatorId",
                table: "Feedback",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Users_CreatorId",
                table: "Shop",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Shop_ShopId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Users_CreatorId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Users_CreatorId",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shop",
                table: "Shop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "EmailCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailIsVerified",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Shop",
                newName: "Shops");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_Shop_CreatorId",
                table: "Shops",
                newName: "IX_Shops_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_ShopId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_CreatorId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_CreatorId");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shops",
                table: "Shops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_DeletorId",
                table: "Shops",
                column: "DeletorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_EditorId",
                table: "Shops",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_DeletorId",
                table: "Feedbacks",
                column: "DeletorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EditorId",
                table: "Feedbacks",
                column: "EditorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Shops_ShopId",
                table: "Feedbacks",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_CreatorId",
                table: "Feedbacks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_DeletorId",
                table: "Feedbacks",
                column: "DeletorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_EditorId",
                table: "Feedbacks",
                column: "EditorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Users_CreatorId",
                table: "Shops",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Users_DeletorId",
                table: "Shops",
                column: "DeletorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Users_EditorId",
                table: "Shops",
                column: "EditorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
