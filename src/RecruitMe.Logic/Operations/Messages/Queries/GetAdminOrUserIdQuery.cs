using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Messages.Queries
{
    public class GetAdminOrUserIdQuery : BaseAsyncOperation<int, string>
    {
        private readonly BusinessConfiguration _businessConfiguration;

        public GetAdminOrUserIdQuery(ILogger logger, BaseDbContext dbContext,
            BusinessConfiguration businessConfiguration) : base(logger, dbContext)
        {
            _businessConfiguration = businessConfiguration;
        }

        public async override Task<int> Execute(string userId)
        {
            int id = -1;

            if (userId == "admin")
            {
                User admin = await _dbContext.Users.FirstOrDefaultAsync(u => u.CandidateId == _businessConfiguration.AdminLogin);
                if (admin == null) throw new Exception("No admin in db");

                id = admin.Id;
            }
            else
            {
                id = int.Parse(userId);
            }

            return id;
        }
    }
}
