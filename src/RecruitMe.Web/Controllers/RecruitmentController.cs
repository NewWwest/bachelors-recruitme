using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Recruitment.Command;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using RecruitMe.Logic.Operations.Recruitment.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class RecruitmentController : RecruitMeBaseController
    {
        private readonly GetPersonalDataQuery _getPersonalDataQuery;
        private readonly AddOrUpdatePersonalDataCommand _addOrUpdatePersonalDataCommand;

        public RecruitmentController(GetPersonalDataQuery getPersonalDataQuery, AddOrUpdatePersonalDataCommand addOrUpdatePersonalDataCommand)
        {
            _getPersonalDataQuery = getPersonalDataQuery;
            _addOrUpdatePersonalDataCommand = addOrUpdatePersonalDataCommand;
        }

        [HttpGet]
        //[Route("PersonalData")]
        //[Authorize]
        public async Task<ActionResult> GetPersonalData()
        {
            int id = 1;
            var result = await _getPersonalDataQuery.Execute(id);
            return Json(result);
        }

        [HttpPost]
        //[Route("PersonalData")]
        //[Authorize]
        public async Task<ActionResult> UpdatePersonalData([FromBody] PersonalDataDto personalData)
        {
            var cmdResult = await _addOrUpdatePersonalDataCommand.Execute(new AddOrUpdatePersonalDataCommandRequest() { UserId = 1, Data = personalData });
            if (cmdResult)
            {
                int id = 1;
                var result = await _getPersonalDataQuery.Execute(id);
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
