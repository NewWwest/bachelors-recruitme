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

        public virtual DbSet<User> Users { get; set; }

        public DbSet<PersonalData> PersonalData { get; set; }

        public DbSet<PasswordReset> PasswordResets { get; set; }

        public DbSet<ConfirmationEmail> ConfirmationEmails { get; set; }

        public DbSet<PersonalDocument> PersonalDocuments { get; set; }

        public DbSet<ExamCategory> ExamCategories { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<ExamTaker> ExamTakers { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BaseDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasAlternateKey(c => c.CandidateId);

            modelBuilder.Entity<ExamTaker>()
              .HasOne(et => et.User)
              .WithMany(u => u.ExamTakers)
              .HasForeignKey(et=> et.CandidateId)
              .HasPrincipalKey(u => u.CandidateId);

        }
        public void EnsureCreated()
        {
            Database.EnsureCreated();
            var admin = Users.FirstOrDefault(u => u.CandidateId == BusinessConfiguration.AdminLogin);
            if(admin == null)
            {
                Users.Add(
                    new User
                    {
                        CandidateId = BusinessConfiguration.AdminLogin,
                        Email = BusinessConfiguration.Email,
                        EmailVerified = true,
                        Name = "Administrator",
                        Surname = "Szkoły",
                        PasswordHash = BusinessConfiguration.InitialAdminPasswordHash
                    });
                SaveChanges();
            }
        }
    }
}
