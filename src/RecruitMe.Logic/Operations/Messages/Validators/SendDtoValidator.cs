using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;

namespace RecruitMe.Logic.Operations.Messages.Validators
{
    public class SendDtoValidator : BaseValidator<SendDto>
    {
        public SendDtoValidator()
        {
            RuleFor(a => a.FromId).GreaterThan(0);
            RuleFor(a => a.ToId).Must(t => t == "admin" || int.Parse(t) > 0);
            RuleFor(a => a.Message).NotEmpty();
        }
    }
}
