using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
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

        public AccountController(
            LoginUserQuery loginUserQuery,
            RegisterUserCommand registerUserCommand) : base()
            
        {
            _loginUserQuery = loginUserQuery;
            _registerUserCommand = registerUserCommand;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<User> Login([FromBody] LoginDto model)
        {
            User result = await _loginUserQuery.Execute(model);

            return result;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<User> Register([FromBody] RegisterDto model)
        {
            User result = await _registerUserCommand.Execute(model);

            return result;
        }
    }
}