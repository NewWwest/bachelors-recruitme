using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.Command;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using RecruitMe.Logic.Operations.Recruitment.Queries;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/Recruitment")]
    public class RecruitmentController : RecruitMeBaseController
    {
        private readonly GetPersonalDataQuery _getPersonalDataQuery;
        private readonly AddOrUpdatePersonalDataCommand _addOrUpdatePersonalDataCommand;

        public RecruitmentController(GetPersonalDataQuery getPersonalDataQuery, 
            AddOrUpdatePersonalDataCommand addOrUpdatePersonalDataCommand
            ) : base()
        {
            _getPersonalDataQuery = getPersonalDataQuery;
            _addOrUpdatePersonalDataCommand = addOrUpdatePersonalDataCommand;
        }

        [HttpGet]
        [Route("PersonalData")]
        public async Task<ActionResult> GetPersonalData()
        {
            User user = await GetUser();
            PersonalDataDto result = await _getPersonalDataQuery.Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        [Route("PersonalData")]
        public async Task<ActionResult> UpdatePersonalData([FromBody] PersonalDataDto personalData)
        {
            User user = await GetUser();
            OperationResult cmdResult = await _addOrUpdatePersonalDataCommand.Execute(new AddOrUpdatePersonalDataCommandRequest() { UserId = user.Id, Data = personalData });
            if (cmdResult.Success)
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
