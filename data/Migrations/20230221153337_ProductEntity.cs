using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data.Migrations
{
    /// <inheritdoc />
    public partial class ProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Free = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Extension = table.Column<string>(type: "text", nullable: true),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Commission = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discription = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    EmailIsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    EmailCode = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    Depth = table.Column<double>(type: "double precision", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    PhotoIdId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_FileInfos_PhotoIdId",
                        column: x => x.PhotoIdId,
                        principalTable: "FileInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LogoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Inn = table.Column<string>(type: "text", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    MinPrice = table.Column<double>(type: "double precision", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shop_FileInfos_LogoId",
                        column: x => x.LogoId,
                        principalTable: "FileInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shop_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteShops",
                columns: table => new
                {
                    FavoriteShopsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteShops", x => new { x.FavoriteShopsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_FavoriteShops_Shop_FavoriteShopsId",
                        column: x => x.FavoriteShopsId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteShops_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Stars = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EditorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletorId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShopCategories",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCategories", x => new { x.ShopId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ShopCategories_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopDeliveries",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopDeliveries", x => new { x.ShopId, x.DeliveryId });
                    table.ForeignKey(
                        name: "FK_ShopDeliveries_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopPayments",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Commission = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPayments", x => new { x.ShopId, x.PaymentId });
                    table.ForeignKey(
                        name: "FK_ShopPayments_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopTypes",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTypes", x => new { x.ShopId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_ShopTypes_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteShops_UsersId",
                table: "FavoriteShops",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CreatorId",
                table: "Feedback",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_ShopId",
                table: "Feedback",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CreatorId",
                table: "Product",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PhotoIdId",
                table: "Product",
                column: "PhotoIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_CreatorId",
                table: "Shop",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_LogoId",
                table: "Shop",
                column: "LogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "FavoriteShops");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShopCategories");

            migrationBuilder.DropTable(
                name: "ShopDeliveries");

            migrationBuilder.DropTable(
                name: "ShopPayments");

            migrationBuilder.DropTable(
                name: "ShopTypes");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropTable(
                name: "FileInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
