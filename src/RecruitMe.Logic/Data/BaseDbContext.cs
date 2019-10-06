using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Data
{
    public abstract class BaseDbContext : IdentityDbContext
    {
        public DbSet<PersonalData> PersonalData { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BaseDbContext()
        {
        }
    }
}
