using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using RecruitMe.Web.Configuration;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/Recruitment")]
    public class RecruitmentController : RecruitMeBaseController
    {
        private readonly GetProfileDataQuery _getProfileDataQuery;
        private readonly AddOrUpdateProfileDataCommand _addOrUpdateProfileDataCommand;
        private readonly SetNewProfilePictureCommand _setNewProfilePictureCommand;

        public RecruitmentController(GetProfileDataQuery GetProfileDataQuery, 
            AddOrUpdateProfileDataCommand AddOrUpdateProfileDataCommand,
            SetNewProfilePictureCommand SetNewProfilePictureCommand
            ) : base()
        {
            _getProfileDataQuery = GetProfileDataQuery;
            _addOrUpdateProfileDataCommand = AddOrUpdateProfileDataCommand;
            _setNewProfilePictureCommand = SetNewProfilePictureCommand;
        }

        [HttpGet]
        [Route("PersonalData")]
        public async Task<ActionResult> GetPersonalData()
        {
            User user = await GetUser();
            ProfileDataDto result = await _getProfileDataQuery.Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        [Route("PersonalData")]
        public async Task<ActionResult> UpdatePersonalData([FromBody] ProfileDataDto personalData)
        {
            User user = await GetUser();
            OperationResult cmdResult = await _addOrUpdateProfileDataCommand.Execute(new AddOrUpdateProfileDataCommandRequest() { UserId = user.Id, Data = personalData });
            if (cmdResult.Success)
            {
                ProfileDataDto result = await _getProfileDataQuery.Execute(user.Id);
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("ProfilePicture")]
        public async Task<ActionResult> ProfilePicture(IFormFile picture)
        {
            User user = await GetUser();

            using (var stream = picture.OpenReadStream())
            {
                var result = await _setNewProfilePictureCommand.Execute(new SetNewProfilePictureCommandRequest() { 
                    File = stream, 
                    FileName = picture.FileName,
                    UserId = user.Id 
                });
            }
            return Ok();
        }


        [HttpGet]
        [Route(FileStorageConfiguration.ProfilePictures + "{fileId}")]
        public async Task<ActionResult> ProfilePicture(string fileId)
        {
            User user = await GetUser();

            //todo check permission
            return PhysicalFile(fileId, "image");
        }
    }
}
