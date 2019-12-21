using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/administration/examCategory/")]
    public class ExamCategoryController : RecruitMeBaseController
    {
        public ExamCategoryController()
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            await AuthenticateAdmin();

            IEnumerable<ExamCategoryDto> result = await Get<GetExamCategoriesQuery>().Execute();
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            await AuthenticateAdmin();
            IEnumerable<ExamCategoryDto> examCategories = await Get<GetExamCategoriesQuery>().Execute();
            ExamCategoryDto result = examCategories.Single(ec => ec.Id == id);

            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> Add([FromBody]ExamCategoryDto examCategory)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<AddExamCategoryCommand>().Execute(examCategory);
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
        public async Task<ActionResult> Update([FromBody]ExamCategoryDto examCategory)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<UpdateExamCategoryCommand>().Execute(examCategory);
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

            OperationResult result = await Get<DeleteExamCategoryCommand>().Execute(id);
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
