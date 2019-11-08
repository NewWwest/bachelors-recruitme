using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class AddOrUpdateProfileDataCommandRequestValidator : BaseValidator<AddOrUpdateProfileDataCommandRequest>
    {
        public AddOrUpdateProfileDataCommandRequestValidator()
        {
            RuleFor(a => a.UserId).GreaterThan(0);
            RuleFor(a => a.Data).NotNull();
        }
    }
}
