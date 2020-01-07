using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;

namespace RecruitMe.Logic.Operations.Messages.Validators
{
    public class GetMessagesValidator : BaseValidator<GetMessagesDto>
    {
        public GetMessagesValidator()
        {
            RuleFor(a => a.From).GreaterThan(0).WithMessage("Nadawca wiadomości musi być wskazany");
            RuleFor(a => a.To).GreaterThan(0).WithMessage("Odbiorca wiadomości musi być wskazany");
            RuleFor(a => a.Parameters).NotNull().WithMessage("Nie zostały podane parametry");
            RuleFor(a => a.Parameters.Page).GreaterThanOrEqualTo(0).WithMessage("Strona wiadomości musi być większa od zera");
            RuleFor(a => a.Parameters.PageSize).GreaterThan(0).WithMessage("Liczba ściąganych wiadomości musi być większa od zera");
        }
    }
}
