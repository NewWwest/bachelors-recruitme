using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.MyRecruitment;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using RecruitMe.Web.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/Recruitment")]
    public class RecruitmentController : RecruitMeBaseController
    {
        public RecruitmentController()
        {
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<ActionResult> GetPersonalData()
        {
            User user = await AuthenticateUser();

            ProfileDataDto result = await Get<GetProfileDataQuery>().Execute(user.Id);
            return Json(result);
        }

        [HttpPost]
        [Route("PersonalData")]
        public async Task<ActionResult> UpdatePersonalData([FromBody] ProfileDataDto personalData)
        {
            User user = await AuthenticateUser();
            OperationResult cmdResult = await Get<AddOrUpdateProfileDataCommand>().Execute(
                new AddOrUpdateProfileDataCommandRequest()
                {
                    UserId = user.Id,
                    Data = personalData
                });

            if (cmdResult.Success)
            {
                ProfileDataDto result = await Get<GetProfileDataQuery>().Execute(user.Id);
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

            using (Stream stream = picture.OpenReadStream())
            {
                string result = await Get<SetNewProfilePictureCommand>().Execute(
                    new FileRequest()
                    {
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

            using (Stream stream = file.OpenReadStream())
            {
                string result = await Get<SaveFileCommand>().Execute(
                    new FileRequest()
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
            OperationResult result = await Get<DeleteFileCommand>().Execute((user.Id, fileid));

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("examsandstatus")]
        public async Task<ActionResult> GetExamsAndStatus()
        {
            User user = await AuthenticateUser();

            ExamsAndStatusDto result = await Get<GetExamsAndStatusQuery>().Execute(user.Id);
            return Ok(result);
        }
    }
}
