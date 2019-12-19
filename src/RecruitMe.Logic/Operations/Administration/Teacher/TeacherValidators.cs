using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;

namespace RecruitMe.Logic.Operations.Administration.Teacher
{
    public class AddTeacherValidator : BaseValidator<TeacherDto>
    {
        public AddTeacherValidator()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Surname).NotEmpty();
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
            RuleFor(a => a.Id).Null();
        }
    }

    public class UpdateTeacherValidator : BaseValidator<TeacherDto>
    {
        public UpdateTeacherValidator()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Surname).NotEmpty();
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
            RuleFor(a => a.Id).NotNull();
        }
    }
}