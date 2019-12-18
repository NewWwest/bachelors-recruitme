using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/administration/teacher/")]
    public class TeacherController : RecruitMeBaseController
    {
        public TeacherController()
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            await AuthenticateAdmin();

            IEnumerable<TeacherDto> result = await Get<GetTeachersQuery>().Execute();
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            await AuthenticateAdmin();

            IEnumerable<TeacherDto> teachers = await Get<GetTeachersQuery>().Execute();
            var result = teachers.Single(ec => ec.Id == id);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> Add([FromBody]TeacherDto Teacher)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<AddTeacherCommand>().Execute(Teacher);
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
        public async Task<ActionResult> Update([FromBody]TeacherDto Teacher)
        {
            await AuthenticateAdmin();

            OperationResult result = await Get<UpdateTeacherCommand>().Execute(Teacher);
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
            OperationResult result = await Get<DeleteTeacherCommand>().Execute(id);
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
