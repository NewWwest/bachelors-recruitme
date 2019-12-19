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
        private readonly SaveFileCommand _saveFileCommand;
        private readonly DeleteFileCommand _deleteFileCommand;

        public RecruitmentController(GetProfileDataQuery GetProfileDataQuery, 
            AddOrUpdateProfileDataCommand AddOrUpdateProfileDataCommand,
            SetNewProfilePictureCommand SetNewProfilePictureCommand,
            SaveFileCommand saveFileCommand,
            DeleteFileCommand deleteFileCommand
            ) : base()
        {
            _getProfileDataQuery = GetProfileDataQuery;
            _addOrUpdateProfileDataCommand = AddOrUpdateProfileDataCommand;
            _setNewProfilePictureCommand = SetNewProfilePictureCommand;
            _saveFileCommand = saveFileCommand;
            _deleteFileCommand = deleteFileCommand;
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<ActionResult> GetPersonalData()
        {
            User user = await AuthenticateUser();
            ProfileDataDto result = await _getProfileDataQuery.Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        [Route("PersonalData")]
        public async Task<ActionResult> UpdatePersonalData([FromBody] ProfileDataDto personalData)
        {
            User user = await AuthenticateUser();
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
            User user = await AuthenticateUser();
            
            using (var stream = picture.OpenReadStream())
            {
                var result = await _setNewProfilePictureCommand.Execute(new FileRequest() { 
                    ContentType = picture.ContentType,
                    File = stream, 
                    FileName = picture.FileName,
                    UserId = user.Id 
                });
            }
            return Ok();
        }

        [HttpPost]
        [Route("document")]
        public async Task<ActionResult> AddPersonalDocument(IFormFile file)
        {
            User user = await AuthenticateUser();

            using (var stream = file.OpenReadStream())
            {
                var result = await _saveFileCommand.Execute(new FileRequest()
                {
                    ContentType = file.ContentType,
                    File = stream,
                    FileName = file.FileName,
                    UserId = user.Id
                });
            }
            return Ok();
        }

        [HttpDelete]
        [Route("document/{fileid}")]
        public async Task<ActionResult> DeletePersonalDocument(int fileid)
        {
            User user = await AuthenticateUser();
            var result = await _deleteFileCommand.Execute((user.Id, fileid));

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
