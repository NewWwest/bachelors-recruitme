using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class AddExamValidator : BaseValidator<ExamDto>
    {
        public AddExamValidator()
        {
            RuleFor(a => a.SeatCount).GreaterThan(0);
            RuleFor(a => a.StartDateTime).NotEmpty();
            RuleFor(a => a.DurationInMinutes).GreaterThan(0);
            RuleFor(a => a.ExamCategoryId).GreaterThan(0);
            RuleFor(a => a.Id).Null();
        }
    }

    public class UpdateExamValidator : BaseValidator<ExamDto>
    {
        public UpdateExamValidator()
        {
            RuleFor(a => a.SeatCount).GreaterThan(0);
            RuleFor(a => a.StartDateTime).NotEmpty();
            RuleFor(a => a.DurationInMinutes).GreaterThan(0);
            RuleFor(a => a.ExamCategoryId).GreaterThan(0);
            RuleFor(a => a.Id).NotNull();
        }
    }
}