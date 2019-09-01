using RecrutMe.Logic.Operations.Account.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutMe.Logic.Operations.Account.Validators
{
    public class RegisterRequestValidator : BaseValidator<RegisterDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(a => a.Email).NotEmpty();
        }
    }
}
