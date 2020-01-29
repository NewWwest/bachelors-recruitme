using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Administration.ExamCategory
{
    public class AddExamCategoryValidator : BaseValidator<ExamCategoryDto>
    {
        public AddExamCategoryValidator()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.ExamType).IsInEnum();
            RuleFor(a => a.Id).Null();
        }
    }

    public class UpdateExamCategoryValidator : BaseValidator<ExamCategoryDto>
    {
        public UpdateExamCategoryValidator()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.ExamType).IsInEnum();
            RuleFor(a => a.Id).NotNull();
        }
    }
}