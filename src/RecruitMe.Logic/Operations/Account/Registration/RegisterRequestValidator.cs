using FluentValidation;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Account.Registration
{
    public class RegisterRequestValidator : BaseValidator<RegisterDto>
    {
        public RegisterRequestValidator()
        {
            RuleFor(a => a.Email).EmailAddress();
            RuleFor(a => a.Password).MinimumLength(7);
            RuleFor(a => a.ConfirmPassword).Must((a, _) => a.ConfirmPassword == a.Password);
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Surname).NotEmpty();
            RuleFor(a => a.BirthDate).NotNull()
                .InclusiveBetween(BusinessConfiguration.LowestRegistrationDate, BusinessConfiguration.HighestRegistrationDate);
        }
    }
}
