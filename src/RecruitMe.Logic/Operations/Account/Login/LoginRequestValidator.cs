using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Account.Login
{
    public class LoginRequestValidator : BaseValidator<LoginDto>
    {
        public LoginRequestValidator() 
        {
            RuleFor(a => a.CandidateId).NotEmpty().WithMessage("Login jest wymagany.");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Hasło jest wymagane."); ;
        }
    }
}
