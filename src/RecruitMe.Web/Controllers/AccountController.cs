using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account;
using RecruitMe.Logic.Operations.Account.Login;
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
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            int result = await Get<RegisterUserCommand>().Execute(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("ConfirmEmail/{token}")]
        public async Task<ActionResult> ConfirmEmail(Guid token)
        {
            string candidateId = await Get<ConfirmEmailCommand>().Execute(token);
            if (string.IsNullOrWhiteSpace(candidateId))
            {
                throw new Exception("Returned login was empty");
            }

            return Redirect(Get<EndpointConfig>().EmailVerified(candidateId));
        }

        [HttpPost]
        [Route("SetNewPassword")]
        public async Task<ActionResult> SetNewPassword([FromBody]SetNewPasswordDto request)
        {
            await Get<SetNewPasswordCommand>().Execute(request);
            return Ok();
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPasswordDto login)
        {
            try
            {
                await Get<ResetPasswordCommand>().Execute(login);
                return Ok();

            }
            catch (ValidationFailedException e)
            {
                return BadRequest(e.ValidationResult.Errors);
            }
            catch (Exception)
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
                await Get<RemindLoginCommand>().Execute(request);
                return Ok();
            }
            catch (ValidationFailedException e)
            {
                return BadRequest(e.ValidationResult.Errors);
            }
            catch (Exception)
            {
                //Hide error to block looking up emails
                return Ok();
            }
        }
    }
}