using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account.Commands;
using RecruitMe.Logic.Operations.Account.Dto;
using RecruitMe.Logic.Operations.Account.Queries;

namespace RecruitMe.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/Account")]
    public class AccountController : RecruitMeBaseController
    {
        private readonly LoginUserQuery _loginUserQuery;
        private readonly RegisterUserCommand _registerUserCommand;
        private readonly ConfirmEmailCommand _confirmEmailCommand;

        public AccountController(
            LoginUserQuery loginUserQuery,
            RegisterUserCommand registerUserCommand, ConfirmEmailCommand confirmEmailCommand) : base()
            
        {
            _loginUserQuery = loginUserQuery;
            _registerUserCommand = registerUserCommand;
            _confirmEmailCommand = confirmEmailCommand;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<int> Register([FromBody] RegisterDto model)
        {
            var result = await _registerUserCommand.Execute(model);

            return result;
        }

        [HttpGet]
        [Route("ConfirmEmail/{token}")]
        public async Task<ActionResult> ConfirmEmail(Guid token)
        {
            //try
            //{
                string candidateId = await _confirmEmailCommand.Execute(token);
                if (!string.IsNullOrWhiteSpace(candidateId))
                    return Redirect($"/EmailVerified?candidateId={candidateId}");
                else
                    return BadRequest();
            //}
            //catch
            //{
            //    return BadRequest();
            //}

        }
    }
}