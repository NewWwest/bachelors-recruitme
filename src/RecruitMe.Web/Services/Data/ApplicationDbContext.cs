using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;

namespace RecruitMe.Web.Services.Data
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(BusinessConfiguration businessConfiguration) : base(businessConfiguration)
        {
        }

        public ApplicationDbContext(DbContextOptions options, BusinessConfiguration businessConfiguration) : base(options, businessConfiguration)
        {
        }
    }
}
