using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Exam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/administration/Exam/")]
    public class ExamController : RecruitMeBaseController
    {
        public ExamController()
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            await AuthenticateAdmin();

            IEnumerable<ExamDto> result = await Get<GetExamsQuery>().Execute();
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            await AuthenticateAdmin();

            ExamDetailsDto result = await Get<GetExamDetailsQuery>().Execute(id);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> Add([FromBody]ExamDto Exam)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<AddExamCommand>().Execute(Exam);
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
            await AuthenticateAdmin();

            OperationResult result = await Get<UpdateExamCommand>().Execute(Exam);
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
            await AuthenticateAdmin();

            OperationResult result = await Get<DeleteExamCommand>().Execute(id);
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
        [Route("{id}/sheet")]
        public async Task<ActionResult> DownloadExamSheet(int id)
        {
            await AuthenticateAdmin();

            Stream result = await Get<GetExaminationSheetQuery>().Execute(id);
            return new FileStreamResult(result, "application/pdf");
        }

        [HttpPost]
        [Route("{id}/sheet")]
        public async Task<ActionResult> UploadExamSheet(int id, IFormFile file, int teacherId)
        {
            await AuthenticateAdmin();
            using (var fileStream = file.OpenReadStream())
            {

                OperationResult result = Get<LoadExaminationSheetCommand>().Execute(new LoadExaminationSheetRequest()
                {
                    ExamId = id,
                    TeacherId = teacherId,
                    FileStream = fileStream
                });

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
}
