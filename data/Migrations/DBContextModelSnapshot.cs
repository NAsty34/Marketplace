﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using data;

#nullable disable

namespace data.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShopUser", b =>
                {
                    b.Property<Guid>("FavoriteShopsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("FavoriteShopsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("FavoriteShops", (string)null);
                });

            modelBuilder.Entity("data.model.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("parentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("parentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("data.model.DeliveryType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Free")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DeliveryTypes");
                });

            modelBuilder.Entity("data.model.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uuid");

                    b.Property<int>("Stars")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ShopId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("data.model.FileInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileInfos");
                });

            modelBuilder.Entity("data.model.PaymentMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Commission")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("data.model.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LogoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("isPublic")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LogoId");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("data.model.ShopCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("shopid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("shopid");

                    b.ToTable("ShopCategories");
                });

            modelBuilder.Entity("data.model.ShopDelivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("shopid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("shopid");

                    b.ToTable("ShopDeliveries");
                });

            modelBuilder.Entity("data.model.ShopPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Paymentid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("shopid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("shopid");

                    b.ToTable("ShopPayments");
                });

            modelBuilder.Entity("data.model.ShopTypes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("shopid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("shopid");

                    b.ToTable("ShopTypes");
                });

            modelBuilder.Entity("data.model.Type", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("data.model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DeletorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EmailCode")
                        .HasColumnType("text");

                    b.Property<bool>("EmailIsVerified")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8320cb9e-0139-43cf-bae0-373dda4ddfce"),
                            CreateDate = new DateTime(2023, 2, 1, 14, 19, 56, 82, DateTimeKind.Local).AddTicks(7665),
                            Email = "admin@gmail.com",
                            EmailIsVerified = true,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Admin",
                            Password = "$2a$11$rBFRPBhwVU1ovzewlDLns.9jPc4mmnNs5g3NHD/NlGYZUbsEaq0Zy",
                            Patronymic = "Admin",
                            Role = "Admin",
                            Surname = "Admin"
                        });
                });

            modelBuilder.Entity("ShopUser", b =>
                {
                    b.HasOne("data.model.Shop", null)
                        .WithMany()
                        .HasForeignKey("FavoriteShopsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("data.model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("data.model.Category", b =>
                {
                    b.HasOne("data.model.Category", "parent")
                        .WithMany()
                        .HasForeignKey("parentId");

                    b.Navigation("parent");
                });

            modelBuilder.Entity("data.model.Feedback", b =>
                {
                    b.HasOne("data.model.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("data.model.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("data.model.Shop", b =>
                {
                    b.HasOne("data.model.User", "Creator")
                        .WithMany("Shops")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("data.model.FileInfo", "Logo")
                        .WithMany()
                        .HasForeignKey("LogoId");

                    b.Navigation("Creator");

                    b.Navigation("Logo");
                });

            modelBuilder.Entity("data.model.ShopCategory", b =>
                {
                    b.HasOne("data.model.Shop", null)
                        .WithMany("ShopCategory")
                        .HasForeignKey("shopid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("data.model.ShopDelivery", b =>
                {
                    b.HasOne("data.model.Shop", null)
                        .WithMany("ShopDeliveries")
                        .HasForeignKey("shopid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("data.model.ShopPayment", b =>
                {
                    b.HasOne("data.model.Shop", null)
                        .WithMany("ShopPayment")
                        .HasForeignKey("shopid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("data.model.ShopTypes", b =>
                {
                    b.HasOne("data.model.Shop", null)
                        .WithMany("ShopTypes")
                        .HasForeignKey("shopid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("data.model.Shop", b =>
                {
                    b.Navigation("ShopCategory");

                    b.Navigation("ShopDeliveries");

                    b.Navigation("ShopPayment");

                    b.Navigation("ShopTypes");
                });

            modelBuilder.Entity("data.model.User", b =>
                {
                    b.Navigation("Shops");
                });
#pragma warning restore 612, 618
        }
    }
}
