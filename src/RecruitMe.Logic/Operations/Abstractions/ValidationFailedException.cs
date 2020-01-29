using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Abstractions
{
    public class ValidationFailedException : Exception
    {
        public ValidationResult ValidationResult { get; set; }
    }
}
