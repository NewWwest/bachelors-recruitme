using RecruitMe.Logic.Operations.Account.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Account.Validators
{
    public class LoginRequestValidator : BaseValidator<LoginDto>
    {
        public LoginRequestValidator() 
        {
            RuleFor(a => a.CandidateId).NotEmpty();
            RuleFor(a => a.Password).NotEmpty();
        }
    }
}
