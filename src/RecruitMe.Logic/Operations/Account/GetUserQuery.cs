using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account
{
    public class GetUserQuery : BaseAsyncOperation<User, int>
    {
        public GetUserQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override Task<User> Execute(int id)
        {
            return _dbContext.Users.SingleAsync(u => u.Id == id);
        }
    }
}
