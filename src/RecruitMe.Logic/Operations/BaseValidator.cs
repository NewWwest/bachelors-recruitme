using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations
{
    public abstract class BaseValidator<TRequest> : AbstractValidator<TRequest>
    {
        public ValidationResult ValidateRequest(TRequest request)
        {
            FluentValidation.Results.ValidationResult result = base.Validate(request);
            return new ValidationResult(result);
        }
    }
}
