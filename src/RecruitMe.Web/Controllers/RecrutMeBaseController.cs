using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Account.Queries;
using System;
using System.Collections.Generic;
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
    }
}
