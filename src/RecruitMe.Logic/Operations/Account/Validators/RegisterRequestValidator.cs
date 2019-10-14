using RecruitMe.Logic.Operations.Account.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Validators
{
    public class RegisterRequestValidator : BaseValidator<RegisterDto>
    {
        public RegisterRequestValidator()
        {
            //TODO Update
            RuleFor(a => a.Email).NotEmpty();
        }
    }
}
