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
        private const string C_ADMIN_USERID = "admin";
        private readonly BusinessConfiguration _businessConfiguration;

        public GetAdminOrUserIdQuery(ILogger logger, BaseDbContext dbContext,
            BusinessConfiguration businessConfiguration) : base(logger, dbContext)
        {
            _businessConfiguration = businessConfiguration;
        }

        public async override Task<int> Execute(string userId)
        {
            int id = -1;

            if (userId == C_ADMIN_USERID)
            {
                User admin = await _dbContext.Users.FirstOrDefaultAsync(u => u.CandidateId == _businessConfiguration.AdminLogin);
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
