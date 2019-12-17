using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Candidate;
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

        public CandidatesController(
            GetCandidatesQuery GetCandidatesQuery,
            GetProfileDataQuery GetProfileDataQuery,
            UpdateCandidateCommand UpdateCandidateCommand,
            DeleteCandidateCommand DeleteCandidateCommand,
            DeleteFileCommand deleteFileCommand)
        {
            _GetCandidatesQuery = GetCandidatesQuery;
            _GetProfileDataQuery = GetProfileDataQuery;
            _UpdateCandidateCommand = UpdateCandidateCommand;
            _DeleteCandidateCommand = DeleteCandidateCommand;
            _deleteFileCommand = deleteFileCommand;
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
    }

}
