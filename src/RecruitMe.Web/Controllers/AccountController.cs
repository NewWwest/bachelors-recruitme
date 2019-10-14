using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Account.Commands;
using RecruitMe.Logic.Operations.Account.Dto;
using RecruitMe.Logic.Operations.Account.Queries;

namespace RecruitMe.Web.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AccountController : RecruitMeBaseController
    {
        private readonly LoginUserQuery _loginUserQuery;
        private readonly RegisterUserCommand _registerUserCommand;

        public AccountController(
            LoginUserQuery loginUserQuery,
            RegisterUserCommand registerUserCommand,
            GetCurrentUserQuery getCurrentUserQuery) : base(getCurrentUserQuery)
            
        {
            _loginUserQuery = loginUserQuery;
            _registerUserCommand = registerUserCommand;
        }

        [HttpPost]
        public async Task<LoggedInUserDto> Login([FromBody] LoginDto model)
        {
            LoggedInUserDto result = await _loginUserQuery.Execute(model);

            return result;
        }

        [HttpPost]
        public async Task<LoggedInUserDto> Register([FromBody] RegisterDto model)
        {
            LoggedInUserDto result = await _registerUserCommand.Execute(model);

            return result;
        }
    }
}