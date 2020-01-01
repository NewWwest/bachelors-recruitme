using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;

namespace RecruitMe.Logic.Operations.Messages.Validators
{
    public class GetMessagesValidator : BaseValidator<GetMessagesDto>
    {
        public GetMessagesValidator()
        {
            RuleFor(a => a.From).GreaterThan(0);
            RuleFor(a => a.To).GreaterThan(0);
            RuleFor(a => a.Parameters).NotNull();
            RuleFor(a => a.Parameters.Page).GreaterThanOrEqualTo(0);
            RuleFor(a => a.Parameters.PageSize).GreaterThan(0);
        }
    }
}
