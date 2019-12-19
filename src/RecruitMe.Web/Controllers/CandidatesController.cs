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
        private readonly GetCandidatesQuery _GetCandidatesQuery;
        private readonly GetProfileDataQuery _GetProfileDataQuery;
        private readonly UpdateCandidateCommand _UpdateCandidateCommand;
        private readonly DeleteCandidateCommand _DeleteCandidateCommand;
        private readonly DeleteFileCommand _deleteFileCommand;
        private readonly GetEnrolledExamsQuerry _GetEnrolledExamsQuerry;
        private readonly AddOrUpdateExamTakerCommand _AddOrUpdateExamTakerCommand;
        private readonly DeleteExamTakerCommand _DeleteExamTakerCommand;

        public CandidatesController(
            GetCandidatesQuery GetCandidatesQuery,
            GetProfileDataQuery GetProfileDataQuery,
            UpdateCandidateCommand UpdateCandidateCommand,
            DeleteCandidateCommand DeleteCandidateCommand,
            DeleteFileCommand deleteFileCommand,
            GetEnrolledExamsQuerry GetEnrolledExamsQuerry,
            AddOrUpdateExamTakerCommand AddOrUpdateExamTakerCommand,
            DeleteExamTakerCommand DeleteExamTakerCommand)
        {
            _GetCandidatesQuery = GetCandidatesQuery;
            _GetProfileDataQuery = GetProfileDataQuery;
            _UpdateCandidateCommand = UpdateCandidateCommand;
            _DeleteCandidateCommand = DeleteCandidateCommand;
            _deleteFileCommand = deleteFileCommand;
            _GetEnrolledExamsQuerry = GetEnrolledExamsQuerry;
            _AddOrUpdateExamTakerCommand = AddOrUpdateExamTakerCommand;
            _DeleteExamTakerCommand = DeleteExamTakerCommand;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List(PagingParameters paging)
        {
            var admin = await AuthenticateAdmin();
            var data = _GetCandidatesQuery.Execute(paging);
            return Json(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var admin = await AuthenticateAdmin();

            ProfileDataDto data = await _GetProfileDataQuery.Execute(id);
            return Json(data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Update([FromBody]ProfileDataDto data)
        {
            var admin = await AuthenticateAdmin();

            OperationResult cmdResult = await _UpdateCandidateCommand.Execute(data);
            ProfileDataDto qResult = await _GetProfileDataQuery.Execute(data.UserId);

            return Json(qResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var admin = await AuthenticateAdmin();

            var result = await _DeleteCandidateCommand.Execute(id);
            return Json(result);
        }

        [HttpDelete]
        [Route("{userId}/documents/{fileid}")]
        public async Task<ActionResult> DeletePersonalDocument(int userId, int fileid)
        {
            var admin = await AuthenticateAdmin();

            var cmdResult = await _deleteFileCommand.Execute((userId, fileid));
            if (cmdResult.Success)
            {
                ProfileDataDto result = await _GetProfileDataQuery.Execute(userId);
                return Json(result);
            }

            return BadRequest();

        }


        [HttpGet]
        [Route("{userId}/exams/")]
        public async Task<ActionResult> ListExams(int userId)
        {
            var admin = await AuthenticateAdmin();

            var data = await _GetEnrolledExamsQuerry.Execute(userId);
            return Json(data);
        }

        [HttpPost]
        [Route("{userId}/exams/")]
        public async Task<ActionResult> AddOrUpdateExamTaker([FromBody]ExamTakerDto examTaker)
        {
            var admin = await AuthenticateAdmin();

            var id = await _AddOrUpdateExamTakerCommand.Execute(examTaker);
            var data = await _GetEnrolledExamsQuerry.Execute(examTaker.UserId);

            return Json(data);
        }
        [HttpDelete]
        [Route("{userId}/exams/{id}/")]
        public async Task<ActionResult> AddOrUpdateExamTaker(int userId, int id)
        {
            var admin = await AuthenticateAdmin();

            var result = await _DeleteExamTakerCommand.Execute(id);
            var data = await _GetEnrolledExamsQuerry.Execute(userId);

            return Json(data);
        }
    }

}
