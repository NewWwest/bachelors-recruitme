using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecrutMe.Logic.Operations.Account.Commands;
using RecrutMe.Logic.Operations.Account.Dto;
using RecrutMe.Logic.Operations.Account.Queries;

namespace RecrutMe.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : RecrutMeBaseController
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