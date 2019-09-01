﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecrutMe.Logic.Logging;
using RecrutMe.Logic.Operations.Account.Dto;
using RecrutMe.Logic.Operations.Account.Helpers;
using RecrutMe.Logic.Operations.Account.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrutMe.Logic.Operations.Account.Queries
{
    public class LoginUserQuery : BaseAsyncOperation<string, LoginDto, LoginRequestValidator>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public LoginUserQuery(ILogger logger, 
            LoginRequestValidator validator,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            JwtTokenHelper jwtTokenHelper) : base(logger, validator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenHelper = jwtTokenHelper;
        }


        protected override async Task<string> DoExecute(LoginDto request)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.SingleOrDefaultAsync(r => r.Email == request.Email);
                return _jwtTokenHelper.GenerateJwtToken(request.Email, appUser);
            }

            throw new Exception("Login Failed");
        }
    }
}
