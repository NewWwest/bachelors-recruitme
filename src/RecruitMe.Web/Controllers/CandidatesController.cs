using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Candidate;
using RecruitMe.Logic.Operations.Administration.Exam;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using RecruitMe.Logic.Operations.Recruitment.ProfileFiles;
using RecruitMe.Logic.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/administration/candidates/")]
    public class CandidatesController : RecruitMeBaseController
    {
        public CandidatesController()
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List(PagingParameters paging)
        {
            await AuthenticateAdmin();

            PagedResponse<GetCandidatesResultDto> data = Get<GetCandidatesQuery>().Execute(paging);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            await AuthenticateAdmin();

            ProfileDataDto data = await Get<GetProfileDataQuery>().Execute(id);
            return Json(data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Update([FromBody]ProfileDataDto data)
        {
            await AuthenticateAdmin();

            OperationResult cmdResult = await Get<UpdateCandidateCommand>().Execute(data);
            if (!cmdResult.Success)
            {
                return BadRequest();
            }

            ProfileDataDto qResult = await Get<GetProfileDataQuery>().Execute(data.UserId);

            return Json(qResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<DeleteCandidateCommand>().Execute(id);
            if (result.Success)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("{userId}/documents/{fileid}")]
        public async Task<ActionResult> DeletePersonalDocument(int userId, int fileid)
        {
            await AuthenticateAdmin();

            OperationResult cmdResult = await Get<DeleteFileCommand>().Execute((userId, fileid));
            if (!cmdResult.Success)
            {
                return BadRequest();
            }

            ProfileDataDto result = await Get<GetProfileDataQuery>().Execute(userId);
            return Json(result);
        }


        [HttpGet]
        [Route("{userId}/exams/")]
        public async Task<ActionResult> ListExams(int userId)
        {
            await AuthenticateAdmin();

            List<ExamTakerDto> data = await Get<GetEnrolledExamsQuerry>().Execute(userId);
            return Json(data);
        }

        [HttpPost]
        [Route("{userId}/exams/")]
        public async Task<ActionResult> AddOrUpdateExamTaker([FromBody]ExamTakerDto examTaker)
        {
            await AuthenticateAdmin();

            int id = await Get<AddOrUpdateExamTakerCommand>().Execute(examTaker);
            if (id > 0)
            {
                List<ExamTakerDto> data = await Get<GetEnrolledExamsQuerry>().Execute(examTaker.UserId);

                return Json(data);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{userId}/exams/{id}/")]
        public async Task<ActionResult> AddOrUpdateExamTaker(int userId, int id)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<DeleteExamTakerCommand>().Execute(id);
            if (result.Success)
            {
                var data = await Get<GetEnrolledExamsQuerry>().Execute(userId);

                return Json(data);
            }
            return BadRequest();
        }
    }
}
