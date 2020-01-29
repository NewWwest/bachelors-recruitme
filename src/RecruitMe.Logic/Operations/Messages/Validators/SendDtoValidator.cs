using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;

namespace RecruitMe.Logic.Operations.Messages.Validators
{
    public class SendDtoValidator : BaseValidator<SendDto>
    {
        public SendDtoValidator()
        {
            RuleFor(a => a.FromId).GreaterThan(0).WithMessage("Nadawca musi być większe od zera");
            RuleFor(a => a.ToId).Must(t => t == "admin" || int.Parse(t) > 0).WithMessage("Odbiorca musi być większy od zera lub adminem");
            RuleFor(a => a.Message).NotEmpty().WithMessage("Tekst wiadomości musi być niepusty");
        }
    }
}
