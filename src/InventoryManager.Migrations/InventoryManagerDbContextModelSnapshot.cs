﻿// <auto-generated />
using InventoryManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventoryManager.src.InventoryManager.Migrations
{
    [DbContext(typeof(InventoryManagerDbContext))]
    partial class InventoryManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("InventoryManager.Models.DeviceType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("DeviceType");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Персональный компьютер"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Сервер"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Коммутатор"
                        });
                });

            modelBuilder.Entity("InventoryManager.Models.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Group");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Техник"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Администратор"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Суперпользователь"
                        });
                });

            modelBuilder.Entity("InventoryManager.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserGroupID")
                        .HasColumnType("int");

                    b.HasKey("Login");

                    b.HasIndex("UserGroupID");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Login = "root",
                            FirstName = "Иван",
                            LastName = "Иванов",
                            MiddleName = "Иванович",
                            Password = "root",
                            UserGroupID = 3
                        });
                });

            modelBuilder.Entity("InventoryManager.Models.User", b =>
                {
                    b.HasOne("InventoryManager.Models.Group", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("InventoryManager.Models.Group", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
