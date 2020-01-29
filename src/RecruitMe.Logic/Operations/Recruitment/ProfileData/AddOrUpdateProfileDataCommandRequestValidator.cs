using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class AddOrUpdateProfileDataCommandRequestValidator : BaseValidator<AddOrUpdateProfileDataCommandRequest>
    {
        public AddOrUpdateProfileDataCommandRequestValidator()
        {
            //No translation - only technical fields validation
            RuleFor(a => a.UserId).GreaterThan(0);
            RuleFor(a => a.Data).NotNull();
        }
    }
}
