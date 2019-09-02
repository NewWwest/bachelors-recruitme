using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Operations.Account.Commands;
using RecruitMe.Logic.Operations.Account.Dto;
using RecruitMe.Logic.Operations.Account.Queries;

namespace RecruitMe.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : RecruitMeBaseController
    {
        private readonly LoginUserQuery _loginUserQuery;
        private readonly RegisterUserCommand _registerUserCommand;

        public AccountController(
            LoginUserQuery loginUserQuery,
            RegisterUserCommand registerUserCommand
            )
        {
            _loginUserQuery = loginUserQuery;
            _registerUserCommand = registerUserCommand;
        }

        [HttpPost]
        public async Task<string> Login([FromBody] LoginDto model)
        {
            var result = await _loginUserQuery.Execute(model);

            return result;
        }

        [HttpPost]
        public async Task<string> Register([FromBody] RegisterDto model)
        {
            var result = await _registerUserCommand.Execute(model);

            return result;
        }
    }
}