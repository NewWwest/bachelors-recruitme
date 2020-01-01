using System.Collections.Generic;
using System.Linq;

namespace RecruitMe.Logic.Operations.Abstractions
{
    public class ValidationResult
    {
        public List<string> Errors { get; set; }

        public bool Success => !Errors.Any();

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public ValidationResult(string error)
        {
            Errors = new List<string>() { error };
        }

        public ValidationResult(IEnumerable<string> errors)
        {
            Errors = errors.ToList();
        }

        public ValidationResult(FluentValidation.Results.ValidationResult validationResult)
        {
            Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }
}