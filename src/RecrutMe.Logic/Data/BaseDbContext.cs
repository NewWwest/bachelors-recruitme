using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutMe.Logic.Data
{
    public abstract class BaseDbContext : IdentityDbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BaseDbContext()
        {
        }
    }
}
