using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Queries
{
    public class GetUserFromClaimsQuery : BaseAsyncOperation<User, IEnumerable<Claim>>
    {
        public GetUserFromClaimsQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override async Task<User> DoExecute(IEnumerable<Claim> request)
        {
            string email = request.Single(c => c.Type == JwtClaims.ClaimEmail).Value;
            int id = int.Parse(request.Single(c => c.Type == JwtClaims.ClaimId).Value);

            User user = await _dbContext.Users.SingleAsync(u => u.Email == email && u.Id == id);

            return user;
        }
    }
}
