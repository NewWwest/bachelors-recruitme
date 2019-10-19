using RecruitMe.Logic.Operations.Recruitment.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Recruitment.Validators
{
    public class AddOrUpdatePersonalDataCommandRequestValidator : BaseValidator<AddOrUpdatePersonalDataCommandRequest>
    {
        public AddOrUpdatePersonalDataCommandRequestValidator()
        {
            RuleFor(a => a.UserId).GreaterThan(0);
            RuleFor(a => a.Data).NotNull();
        }
    }
}
