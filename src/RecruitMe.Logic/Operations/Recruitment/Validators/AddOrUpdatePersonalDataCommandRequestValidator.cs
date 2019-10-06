using RecruitMe.Logic.Operations.Recruitment.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RecruitMe.Logic.Operations.Recruitment.Validators
{
    public class AddOrUpdatePersonalDataCommandRequestValidator : BaseValidator<AddOrUpdatePersonalDataCommandRequest>
    {
        public AddOrUpdatePersonalDataCommandRequestValidator()
        {
            RuleFor(a => a.UserId).GreaterThan(0);
            RuleFor(a => a.Data).NotNull();
            RuleFor(a => a.Data.Name).NotNull().NotEmpty();
            RuleFor(a => a.Data.Surname).NotNull().NotEmpty();
        }
    }
}
