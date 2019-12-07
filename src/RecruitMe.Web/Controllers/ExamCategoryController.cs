using Microsoft.AspNetCore.Mvc;
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
        private readonly AddExamCategoryCommand _AddExamCategoryCommand;
        private readonly DeleteExamCategoryCommand _DeleteExamCategoryCommand;
        private readonly GetExamCategoriesQuery _GetExamCategoriesQuery;
        private readonly UpdateExamCategoryCommand _UpdateExamCategoryCommand;

        public ExamCategoryController(
AddExamCategoryCommand AddExamCategoryCommand,
DeleteExamCategoryCommand DeleteExamCategoryCommand,
GetExamCategoriesQuery GetExamCategoriesQuery,
UpdateExamCategoryCommand UpdateExamCategoryCommand
            )
        {
            _AddExamCategoryCommand = AddExamCategoryCommand;
            _DeleteExamCategoryCommand = DeleteExamCategoryCommand;
            _GetExamCategoriesQuery = GetExamCategoriesQuery;
            _UpdateExamCategoryCommand = UpdateExamCategoryCommand;
        }
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            var admin = await AuthenticateAdmin();
            var result = await _GetExamCategoriesQuery.Execute();
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var admin = await AuthenticateAdmin();
            var examCategories = await _GetExamCategoriesQuery.Execute();
            var result = examCategories.Single(ec => ec.Id == id);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> Add([FromBody]ExamCategoryDto examCategory)
        {
            var admin = await AuthenticateAdmin();
            var result = await _AddExamCategoryCommand.Execute(examCategory);
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
            var admin = await AuthenticateAdmin();
            var result = await _UpdateExamCategoryCommand.Execute(examCategory);
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
            var result = await _DeleteExamCategoryCommand.Execute(id);
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
