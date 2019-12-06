using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Account;
using RecruitMe.Logic.Utilities;
using RecruitMe.Web.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Authorize]
    public class RecruitMeBaseController : Controller
    {
        protected int UserId => int.Parse(User.Claims.Single(c => c.Type == JwtClaims.ClaimUserId).Value);

        protected async Task<User> AuthenticateUser()
        {
            var query = HttpContext.RequestServices.Get<GetUserQuery>();
            return await query.Execute(UserId);
        }

        protected async Task<User> AuthenticateAdmin()
        {
            var query = HttpContext.RequestServices.Get<GetUserQuery>();
            var user = await query.Execute(UserId);
            if (user.CandidateId == "admin")
            {

                return user;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        public RecruitMeBaseController()
        {
        }
    }
}
