﻿using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
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

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentLink> PaymentLinks { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BaseDbContext()
        {
        }
    }
}
