using Microsoft.AspNetCore.Identity;
using RecrutMe.Logic.Logging;
using RecrutMe.Logic.Operations.Account.Dto;
using RecrutMe.Logic.Operations.Account.Helpers;
using RecrutMe.Logic.Operations.Account.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecrutMe.Logic.Operations.Account.Commands
{
    public class RegisterUserCommand : BaseAsyncOperation<string, RegisterDto, RegisterRequestValidator>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public RegisterUserCommand(ILogger logger, 
            RegisterRequestValidator validator,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            JwtTokenHelper jwtTokenHelper) : base(logger, validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenHelper = jwtTokenHelper;
        }

        protected async override Task<string> DoExecute(RegisterDto request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return _jwtTokenHelper.GenerateJwtToken(request.Email, user);
            }

            throw new Exception("Login Failed");
        }
    }
}
