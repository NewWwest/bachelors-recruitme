using RecrutMe.Logic.Operations.Account.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutMe.Logic.Operations.Account.Validators
{
    public class LoginRequestValidator : BaseValidator<LoginDto>
    {
        public LoginRequestValidator() 
        {
            RuleFor(a => a.Email).NotEmpty();
        }
    }
}
