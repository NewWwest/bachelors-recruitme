using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Account.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Data
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly BusinessConfiguration businessConfiguration;

        public virtual DbSet<User> Users { get; set; }

        public DbSet<PersonalData> PersonalData { get; set; }

        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<ConfirmationEmail> ConfirmationEmails { get; set; }

        public DbSet<PersonalDocument> PersonalDocuments { get; set; }

        public DbSet<ExamCategory> ExamCategories { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<ExamTaker> ExamTakers { get; set; }

        public DbSet<Message> Messages { get; set; }

        public BaseDbContext(DbContextOptions options, BusinessConfiguration businessConfiguration) : base(options)
        {
            this.businessConfiguration = businessConfiguration;
        }

        protected BaseDbContext(BusinessConfiguration businessConfiguration)
        {
            this.businessConfiguration = businessConfiguration;
        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
            var admin = Users.FirstOrDefault(u => u.CandidateId == businessConfiguration.AdminLogin);
            if(admin == null)
            {
                Users.Add(
                    new User
                    {
                        CandidateId = businessConfiguration.AdminLogin,
                        Email = businessConfiguration.Email,
                        EmailVerified = true,
                        Name = "Administrator",
                        Surname = "Szkoły",
                        PasswordHash = businessConfiguration.InitialAdminPasswordHash
                    });
                SaveChanges();
            }
        }
    }
}
