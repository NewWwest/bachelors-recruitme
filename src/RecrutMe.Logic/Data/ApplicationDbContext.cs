using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using RecrutMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutMe.Logic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ExampleClass> ExampleClasses { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}
