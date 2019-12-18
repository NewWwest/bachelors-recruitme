using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Configuration;
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
        public RecruitMeBaseController()
        {
        }

        private int ParseUserId()
        {
            return int.Parse(User.Claims.Single(c => c.Type == JwtClaims.ClaimUserId).Value);
        }

        protected T Get<T>()
        {
            return HttpContext.RequestServices.Get<T>();
        }

        protected async Task<User> AuthenticateUser()
        {
            GetUserQuery query = HttpContext.RequestServices.Get<GetUserQuery>();
            return await query.Execute(ParseUserId());
        }

        protected async Task<User> AuthenticateAdmin()
        {
            GetUserQuery query = HttpContext.RequestServices.Get<GetUserQuery>();
            User user = await query.Execute(ParseUserId());
            if (user.CandidateId == BusinessConfiguration.AdminLogin)
            {

                return user;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
