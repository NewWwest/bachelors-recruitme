﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecruitMe.Web.Services.Data;

namespace RecruitMe.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.ConfirmationEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Used");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ConfirmationEmails");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PasswordReset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("InsertDateTime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PasswordResets");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PersonalData", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("Adress");

                    b.Property<string>("FatherName");

                    b.Property<string>("MotherName");

                    b.Property<string>("PrimarySchool");

                    b.HasKey("UserId");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CandidateId");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailVerified");

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Pesel");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.ConfirmationEmail", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PasswordReset", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PersonalData", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
