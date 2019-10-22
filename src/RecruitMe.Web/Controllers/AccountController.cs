using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account.Registration;
using RecruitMe.Logic.Operations.Account.ResetPassword;

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

        public AccountController(
            RegisterUserCommand registerUserCommand, 
            ConfirmEmailCommand confirmEmailCommand,
            ResetPasswordCommand resetPasswordCommand,
            SetNewPasswordCommand setNewPasswordCommand) : base()

        {
            _registerUserCommand = registerUserCommand;
            _confirmEmailCommand = confirmEmailCommand;
            _setNewPasswordCommand = setNewPasswordCommand;
            _resetPasswordCommand = resetPasswordCommand;
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

        [HttpGet]
        [Route("setNewPassword")]
        public async Task<ActionResult> SetNewPassword(SetNewPasswordDto request)
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

        [HttpGet]
        [Route("resetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto request)
        {
            try
            {
                await _resetPasswordCommand.Execute(request);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }

        }
    }
}