using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.RemindLogin
{
    public class RemindLoginValidator : BaseValidator<RemindLoginDto>
    {
        public RemindLoginValidator()
        {
            RuleFor(a => a.Email).EmailAddress().WithMessage("Błędny adres email."); ;
            RuleFor(a => a.Surname).NotEmpty().When(a => a.Pesel == null).WithMessage("Pesel jest wymagany lub imię i nazwisko są wymane.");
            RuleFor(a => a.Name).NotEmpty().When(a => a.Pesel == null).WithMessage("Pesel jest wymagany lub imię i nazwisko są wymane.");
        }
    }
}
