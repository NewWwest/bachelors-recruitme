using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.SetNewPassword
{
    public class SetNewPasswordValidator : BaseValidator<SetNewPasswordDto>
    {
        public SetNewPasswordValidator()
        {
            RuleFor(a => a.Password).MinimumLength(7).WithMessage("Za którtkie hasło.");
            RuleFor(a => a.ConfirmPassword).Must((a, _) => a.ConfirmPassword == a.Password).WithMessage("Hasła się nie zgadzają.");
            RuleFor(a => a.Token).NotEmpty().WithMessage("Nieprawidłowy link resetujący hasło.");
        }
    }
}
