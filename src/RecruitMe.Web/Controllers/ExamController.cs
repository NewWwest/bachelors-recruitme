using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Administration.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/administration/Exam/")]
    public class ExamController : RecruitMeBaseController
    {
        private readonly AddExamCommand _AddExamCommand;
        private readonly DeleteExamCommand _DeleteExamCommand;
        private readonly GetExamsQuery _GetExamsQuery;
        private readonly UpdateExamCommand _UpdateExamCommand;
        private readonly GetExamDetailsQuery _GetExamDetailsQuery;

        public ExamController(
AddExamCommand AddExamCommand,
DeleteExamCommand DeleteExamCommand,
GetExamsQuery GetExamsQuery,
UpdateExamCommand UpdateExamCommand,
GetExamDetailsQuery GetExamDetailsQuery
            )
        {
            _AddExamCommand = AddExamCommand;
            _DeleteExamCommand = DeleteExamCommand;
            _GetExamsQuery = GetExamsQuery;
            _UpdateExamCommand = UpdateExamCommand;
            _GetExamDetailsQuery = GetExamDetailsQuery;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            var admin = await AuthenticateAdmin();
            var result = await _GetExamsQuery.Execute();
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var admin = await AuthenticateAdmin();
            var result = await _GetExamDetailsQuery.Execute(id);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> Add([FromBody]ExamDto Exam)
        {
            var admin = await AuthenticateAdmin();
            var result = await _AddExamCommand.Execute(Exam);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Update([FromBody]ExamDto Exam)
        {
            var admin = await AuthenticateAdmin();
            var result = await _UpdateExamCommand.Execute(Exam);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var admin = await AuthenticateAdmin();
            var result = await _DeleteExamCommand.Execute(id);
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
