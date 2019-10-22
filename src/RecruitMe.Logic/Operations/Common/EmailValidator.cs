using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Common
{
    public class EmailValidator : BaseValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(s => s).EmailAddress();
        }
    }
}
