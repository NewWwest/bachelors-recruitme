using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/Recruitment")]
    public class RecruitmentController : RecruitMeBaseController
    {
        private readonly GetProfileDataQuery _GetProfileDataQuery;
        private readonly AddOrUpdateProfileDataCommand _AddOrUpdateProfileDataCommand;
        private readonly SetNewProfilePictureCommand _SetNewProfilePictureCommand;

        public RecruitmentController(GetProfileDataQuery GetProfileDataQuery, 
            AddOrUpdateProfileDataCommand AddOrUpdateProfileDataCommand,
            SetNewProfilePictureCommand SetNewProfilePictureCommand
            ) : base()
        {
            _GetProfileDataQuery = GetProfileDataQuery;
            _AddOrUpdateProfileDataCommand = AddOrUpdateProfileDataCommand;
            _SetNewProfilePictureCommand = SetNewProfilePictureCommand;
        }

        [HttpGet]
        [Route("PersonalData")]
        public async Task<ActionResult> GetPersonalData()
        {
            User user = await GetUser();
            ProfileDataDto result = await _GetProfileDataQuery.Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        [Route("PersonalData")]
        public async Task<ActionResult> UpdatePersonalData([FromBody] ProfileDataDto personalData)
        {
            User user = await GetUser();
            OperationResult cmdResult = await _AddOrUpdateProfileDataCommand.Execute(new AddOrUpdateProfileDataCommandRequest() { UserId = user.Id, Data = personalData });
            if (cmdResult.Success)
            {
                ProfileDataDto result = await _GetProfileDataQuery.Execute(user.Id);
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("ProfilePicture")]
        public async Task<ActionResult> ProfilePicture(string picture)
        {
            User user = await GetUser();

            //using (var stream = picture.OpenReadStream())
            //{
            //    var result = await _SetNewProfilePictureCommand.Execute(new SetNewProfilePictureCommandRequest() { 
            //        PictureStream = stream, 
            //        UserId = user.Id 
            //    });
            //}
            return Ok();
        }
    }
}
