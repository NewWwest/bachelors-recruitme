using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account.Registration;
using RecruitMe.Logic.Operations.Account.RemindLogin;
using RecruitMe.Logic.Operations.Account.ResetPassword;
using RecruitMe.Logic.Operations.Account.SetNewPassword;

namespace RecruitMe.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/Account")]
    public class AccountController : RecruitMeBaseController
    {
        private readonly RegisterUserCommand _registerUserCommand;
        private readonly ConfirmEmailCommand _confirmEmailCommand;
        private readonly SetNewPasswordCommand _setNewPasswordCommand;
        private readonly ResetPasswordCommand _resetPasswordCommand;
        private readonly RemindLoginCommand _remindLoginQuery;

        public AccountController(
            RegisterUserCommand registerUserCommand, 
            ConfirmEmailCommand confirmEmailCommand,
            ResetPasswordCommand resetPasswordCommand,
            SetNewPasswordCommand setNewPasswordCommand,
            RemindLoginCommand remindLoginQuery) : base()

        {
            _registerUserCommand = registerUserCommand;
            _confirmEmailCommand = confirmEmailCommand;
            _setNewPasswordCommand = setNewPasswordCommand;
            _resetPasswordCommand = resetPasswordCommand;
            _remindLoginQuery = remindLoginQuery;
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
            try
            {
                string candidateId = await _confirmEmailCommand.Execute(token);
                if (!string.IsNullOrWhiteSpace(candidateId))
                    return Redirect($"/EmailVerified?candidateId={candidateId}");
                else
                    return BadRequest();
            }
            catch
            {
                //todo: remove throw
                throw;
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("SetNewPassword")]
        public async Task<ActionResult> SetNewPassword([FromBody]SetNewPasswordDto request)
        {
            try
            {
                await _setNewPasswordCommand.Execute(request);
                return Ok();
                
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPasswordDto login)
        {
            try
            {
                await _resetPasswordCommand.Execute(login);
                return Ok();

            }
            catch
            {
                //Hide error to block looking up emails
                return Ok();
            }

        }

        [HttpPost]
        [Route("RemindLogin")]
        public async Task<ActionResult> RemindLogin([FromBody]RemindLoginDto request)
        {
            try
            {
                await _remindLoginQuery.Execute(request);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}