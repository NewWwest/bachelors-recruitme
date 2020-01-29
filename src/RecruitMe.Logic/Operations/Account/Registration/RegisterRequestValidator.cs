using FluentValidation;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Account.Registration
{
    public class RegisterRequestValidator : BaseValidator<RegisterDto>
    {
        public RegisterRequestValidator(BusinessConfiguration businessConfiguration)
        {
            RuleFor(a => a.Email).EmailAddress().WithMessage("Niepoprawny email.");
            RuleFor(a => a.Password).MinimumLength(7).WithMessage("Za krótkie hasło.");
            RuleFor(a => a.ConfirmPassword).Must((a, _) => a.ConfirmPassword == a.Password).WithMessage("Hasła się nie zgadzają.");
            RuleFor(a => a.Name).NotEmpty().WithMessage("Imię jest wymagane.");
            RuleFor(a => a.Surname).NotEmpty().WithMessage("Nazwisko jest wymagane.");
            RuleFor(a => a.BirthDate).NotNull().WithMessage("Data urodzenia jest wymagana.")
                .InclusiveBetween(businessConfiguration.LowestRegistrationDate, businessConfiguration.HighestRegistrationDate)
                .WithMessage($"Data urodzenia musi być z przedziału {businessConfiguration.LowestRegistrationDate.ToString("dd.MM.yyyy")} - {businessConfiguration.HighestRegistrationDate.ToString("dd.MM.yyyy")}"); ;
        }
    }
}
