using Microsoft.AspNetCore.Mvc;
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
        private readonly AddTeacherCommand _AddTeacherCommand;
        private readonly DeleteTeacherCommand _DeleteTeacherCommand;
        private readonly GetTeachersQuery _GetTeachersQuery;
        private readonly UpdateTeacherCommand _UpdateTeacherCommand;

        public TeacherController(
AddTeacherCommand AddTeacherCommand,
DeleteTeacherCommand DeleteTeacherCommand,
GetTeachersQuery GetTeachersQuery,
UpdateTeacherCommand UpdateTeacherCommand
            )
        {
            _AddTeacherCommand = AddTeacherCommand;
            _DeleteTeacherCommand = DeleteTeacherCommand;
            _GetTeachersQuery = GetTeachersQuery;
            _UpdateTeacherCommand = UpdateTeacherCommand;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            var admin = await AuthenticateAdmin();
            var result = await _GetTeachersQuery.Execute();
            return Json(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var admin = await AuthenticateAdmin();
            var teachers = await _GetTeachersQuery.Execute();
            var result = teachers.Single(ec => ec.Id == id);
            return Json(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> Add([FromBody]TeacherDto Teacher)
        {
            var admin = await AuthenticateAdmin();
            var result = await _AddTeacherCommand.Execute(Teacher);
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
            var admin = await AuthenticateAdmin();
            var result = await _UpdateTeacherCommand.Execute(Teacher);
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
            var result = await _DeleteTeacherCommand.Execute(id);
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
