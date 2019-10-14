using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account.Dto;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Account.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Commands
{
    public class RegisterUserCommand : BaseAsyncOperation<LoggedInUserDto, RegisterDto, RegisterRequestValidator>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public RegisterUserCommand(ILogger logger, 
            RegisterRequestValidator validator,
            BaseDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtTokenHelper jwtTokenHelper) : base(logger, validator, dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenHelper = jwtTokenHelper;
        }

        protected async override Task<LoggedInUserDto> DoExecute(RegisterDto request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.SingleOrDefaultAsync(r => r.Email == request.Email);

                var loggedInUser = new LoggedInUserDto()
                {
                    Email = appUser.Email,
                    Id = appUser.Id,
                    Token = _jwtTokenHelper.GenerateJwtToken(request.Email, appUser)
                };
                return loggedInUser;
            }

            var errMessage = "Registration Failed: " + string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errMessage);
        }
    }
}
