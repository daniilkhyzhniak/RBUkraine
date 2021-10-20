﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RBUkraine.DAL.Contexts;

namespace RBUkraine.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211020174025_UpdateCharitableOrganizationAndAnimalRelation")]
    partial class UpdateCharitableOrganizationAndAnimalRelation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RBUkraine.DAL.Entities.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharitableOrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LatinName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Population")
                        .HasColumnType("int");

                    b.Property<string>("Species")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharitableOrganizationId");

                    b.ToTable("Animals");

                    b.HasData(
                        new
                        {
                            Id = 1000,
                            CharitableOrganizationId = 1,
                            Description = "Природоохранный статус вида: Пропавший в природе.",
                            IsDeleted = false,
                            LatinName = "Bison bonasus",
                            Name = "Зубр",
                            Population = 200,
                            Species = "Зубр"
                        });
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.AnimalImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("AnimalImages");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.AnimalTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("AnimalTranslates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnimalId = 1000,
                            Description = "Conservation status of the species: Extinct in nature.",
                            IsDeleted = false,
                            Language = 1,
                            Name = "Bison"
                        });
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharitableOrganization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CharitableOrganizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Happy Paw",
                            Email = "happypaw@email.com",
                            IsDeleted = false,
                            Name = "Happy Paw",
                            PhoneNumber = "12345"
                        });
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharitableOrganizationTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharitableOrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharitableOrganizationId");

                    b.ToTable("CharitableOrganizationTranslate");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharityEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organizer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("CharityEvents");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharityEventTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CharityEventId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Organizer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharityEventId");

                    b.ToTable("CharityEventTranslate");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin1@email.com",
                            IsDeleted = false,
                            Password = "$2a$11$U33m8WVMmd68TJ4neIfziOSJMHnF8U1lvQF/a99AW6.jQScQbA1py"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user1@email.com",
                            IsDeleted = false,
                            Password = "$2a$11$MRhltmuO/rreCSmwGdGH5OQAEWBU4DVo085f3ZJ0aQt4rLfQ5Ae5q"
                        });
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            RoleId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            RoleId = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.Animal", b =>
                {
                    b.HasOne("RBUkraine.DAL.Entities.CharitableOrganization", "CharitableOrganization")
                        .WithMany("Animals")
                        .HasForeignKey("CharitableOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharitableOrganization");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.AnimalImage", b =>
                {
                    b.HasOne("RBUkraine.DAL.Entities.Animal", null)
                        .WithMany("AnimalImages")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.AnimalTranslate", b =>
                {
                    b.HasOne("RBUkraine.DAL.Entities.Animal", "Animal")
                        .WithMany("AnimalTranslates")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharitableOrganizationTranslate", b =>
                {
                    b.HasOne("RBUkraine.DAL.Entities.CharitableOrganization", "CharitableOrganization")
                        .WithMany("CharitableOrganizationTranslates")
                        .HasForeignKey("CharitableOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharitableOrganization");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharityEventTranslate", b =>
                {
                    b.HasOne("RBUkraine.DAL.Entities.CharityEvent", "CharityEvent")
                        .WithMany("CharityEventTranslates")
                        .HasForeignKey("CharityEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CharityEvent");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.UserRole", b =>
                {
                    b.HasOne("RBUkraine.DAL.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RBUkraine.DAL.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.Animal", b =>
                {
                    b.Navigation("AnimalImages");

                    b.Navigation("AnimalTranslates");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharitableOrganization", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("CharitableOrganizationTranslates");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.CharityEvent", b =>
                {
                    b.Navigation("CharityEventTranslates");
                });

            modelBuilder.Entity("RBUkraine.DAL.Entities.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
