using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
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
    [Route("api/Recruitment")]
    public class RecruitmentController : RecruitMeBaseController
    {
        private readonly GetPersonalDataQuery _getPersonalDataQuery;
        private readonly AddOrUpdatePersonalDataCommand _addOrUpdatePersonalDataCommand;
        private readonly GetUserFromClaimsQuery _getUserFromClaimsQuery;

        public RecruitmentController(GetPersonalDataQuery getPersonalDataQuery, 
            AddOrUpdatePersonalDataCommand addOrUpdatePersonalDataCommand,
            GetUserFromClaimsQuery getUserFromClaimsQuery
            ) : base()
        {
            _getPersonalDataQuery = getPersonalDataQuery;
            _addOrUpdatePersonalDataCommand = addOrUpdatePersonalDataCommand;
            _getUserFromClaimsQuery = getUserFromClaimsQuery;
        }

        [HttpGet]
        [Route("PersonalData")]
        public async Task<ActionResult> GetPersonalData()
        {
            User user = await _getUserFromClaimsQuery.Execute(User.Claims);
            PersonalDataDto result = await _getPersonalDataQuery.Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        [Route("PersonalData")]
        public async Task<ActionResult> UpdatePersonalData([FromBody] PersonalDataDto personalData)
        {
            User user = await _getUserFromClaimsQuery.Execute(User.Claims);
            bool cmdResult = await _addOrUpdatePersonalDataCommand.Execute(new AddOrUpdatePersonalDataCommandRequest() { UserId = user.Id, Data = personalData });
            if (cmdResult)
            {
                PersonalDataDto result = await _getPersonalDataQuery.Execute(user.Id);
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
