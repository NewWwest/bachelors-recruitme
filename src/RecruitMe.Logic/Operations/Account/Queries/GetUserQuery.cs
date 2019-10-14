using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Queries
{
    public class GetUserQuery : BaseAsyncOperation<User, int>
    {
        public GetUserQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override Task<User> DoExecute(int id)
        {
            return _dbContext.Users.SingleAsync(u => u.Id == id);
        }
    }
}
