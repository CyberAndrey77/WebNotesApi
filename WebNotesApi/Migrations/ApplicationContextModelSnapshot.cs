﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebNotesApi.Services;

#nullable disable

namespace WebNotesApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NoteUser", b =>
                {
                    b.Property<int>("NotesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("NotesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("NoteUser");
                });

            modelBuilder.Entity("WebNotesApi.Models.AutorizationModels.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WebNotesApi.Models.AutorizationModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreationTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateExpirationPasswordVerificationToken")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVerification")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("PasswordVerificationToken")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationTime = "20.03.2023",
                            Email = "lapardin.andrey@mail.ru",
                            IsVerification = false,
                            Name = "Андрей",
                            Password = new byte[] { 54, 39, 144, 154, 41, 195, 19, 129, 160, 113, 236, 39, 247, 201, 202, 151, 114, 97, 130, 174, 210, 154, 125, 221, 46, 84, 53, 51, 34, 207, 179, 10, 187, 158, 58, 109, 242, 172, 44, 32, 254, 35, 67, 99, 17, 214, 120, 86, 77, 12, 141, 48, 89, 48, 87, 95, 96, 226, 211, 208, 72, 24, 77, 121 },
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("WebNotesApi.Models.NoteModels.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NoteName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<string>("UpdatedDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WebNotesApi.Models.NoteModels.NoteCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NoteCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Work"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Home"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "HealthAndSport"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "People"
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Documents"
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Finance"
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Various"
                        });
                });

            modelBuilder.Entity("NoteUser", b =>
                {
                    b.HasOne("WebNotesApi.Models.NoteModels.Note", null)
                        .WithMany()
                        .HasForeignKey("NotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebNotesApi.Models.AutorizationModels.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebNotesApi.Models.AutorizationModels.RefreshToken", b =>
                {
                    b.HasOne("WebNotesApi.Models.AutorizationModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebNotesApi.Models.NoteModels.Note", b =>
                {
                    b.HasOne("WebNotesApi.Models.NoteModels.NoteCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}