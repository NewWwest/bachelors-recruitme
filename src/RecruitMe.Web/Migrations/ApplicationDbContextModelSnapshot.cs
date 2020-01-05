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

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ConfirmationEmails");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DurationInMinutes");

                    b.Property<int>("ExamCategoryId");

                    b.Property<int>("SeatCount");

                    b.Property<DateTime>("StartDateTime");

                    b.HasKey("Id");

                    b.HasIndex("ExamCategoryId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.ExamCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExamType");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ExamCategories");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.ExamTaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExamId");

                    b.Property<float?>("Score");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TeacherId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("UserId");

                    b.ToTable("ExamTakers");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PasswordReset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("InsertDateTime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PasswordResets");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DotpayOperationNumber");

                    b.Property<DateTime>("IssueDate");

                    b.Property<DateTime?>("PaidDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PaymentLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentLinks");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PersonalData", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("Adress");

                    b.Property<string>("FatherName");

                    b.Property<string>("MotherName");

                    b.Property<string>("PrimarySchool");

                    b.Property<int?>("ProfilePictureFileId");

                    b.HasKey("UserId");

                    b.HasIndex("ProfilePictureFileId");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PersonalDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("FileUrl");

                    b.Property<string>("Name");

                    b.Property<int?>("PersonalDataUserId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PersonalDataUserId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonalDocuments");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

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
                        .WithOne("ConfirmationEmail")
                        .HasForeignKey("RecruitMe.Logic.Data.Entities.ConfirmationEmail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.Exam", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.ExamCategory", "ExamCategory")
                        .WithMany("Exams")
                        .HasForeignKey("ExamCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.ExamTaker", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.Exam", "Exam")
                        .WithMany("ExamTakers")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RecruitMe.Logic.Data.Entities.Teacher", "Teacher")
                        .WithMany("ExamTakers")
                        .HasForeignKey("TeacherId");

                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany("ExamTakers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PasswordReset", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithOne("PasswordReset")
                        .HasForeignKey("RecruitMe.Logic.Data.Entities.PasswordReset", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.Payment", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PaymentLink", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PersonalData", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.PersonalDocument", "ProfilePictureFile")
                        .WithMany()
                        .HasForeignKey("ProfilePictureFileId");

                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithOne("PersonalData")
                        .HasForeignKey("RecruitMe.Logic.Data.Entities.PersonalData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RecruitMe.Logic.Data.Entities.PersonalDocument", b =>
                {
                    b.HasOne("RecruitMe.Logic.Data.Entities.PersonalData")
                        .WithMany("Documents")
                        .HasForeignKey("PersonalDataUserId");

                    b.HasOne("RecruitMe.Logic.Data.Entities.User", "User")
                        .WithMany("PersonalDocuments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
