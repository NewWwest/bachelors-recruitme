using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Account.Login
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
