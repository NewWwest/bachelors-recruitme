using RecruitMe.Logic.Data;
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
    public class GetCurrentUserQuery : BaseAsyncOperation<LoggedInUserDto, ClaimsPrincipal>
    {
        public GetCurrentUserQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override Task<LoggedInUserDto> DoExecute(ClaimsPrincipal request)
        {
            var xd = request.Identity;
            Claim xd1 = request.Claims.First();
            var user = new LoggedInUserDto()
            {
                Email = request.Claims.Single(c=>c.Type == ClaimTypes.Email).Value,
                Id = request.Claims.Single(c => xd1.Type == ClaimTypes.NameIdentifier).Value,
                Token = null
            };

            return null;
        }
    }
}
