using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class RecruitmentController : RecruitMeBaseController
    {
        [HttpPost]
        public ActionResult UpdatePersonalData([FromBody] PersonalDataDto personalData)
        {
            return null;
        }
    }
}
