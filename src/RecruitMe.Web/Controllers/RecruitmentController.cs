using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Account.Dto;
using RecruitMe.Logic.Operations.Account.Queries;
using RecruitMe.Logic.Operations.Recruitment.Command;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using RecruitMe.Logic.Operations.Recruitment.Queries;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class RecruitmentController : RecruitMeBaseController
    {
        private readonly GetPersonalDataQuery _getPersonalDataQuery;
        private readonly AddOrUpdatePersonalDataCommand _addOrUpdatePersonalDataCommand;


        public RecruitmentController(GetPersonalDataQuery getPersonalDataQuery, 
            AddOrUpdatePersonalDataCommand addOrUpdatePersonalDataCommand, 
            GetCurrentUserQuery getCurrentUserQuery) : base(getCurrentUserQuery)
        {
            _getPersonalDataQuery = getPersonalDataQuery;
            _addOrUpdatePersonalDataCommand = addOrUpdatePersonalDataCommand;
        }

        [HttpGet]
        //[Route("PersonalData")]
        public async Task<ActionResult> GetPersonalData()
        {
            var user = await _getCurrentUserQuery.Execute(this.User);
            var result = await _getPersonalDataQuery.Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        //[Route("PersonalData")]
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
