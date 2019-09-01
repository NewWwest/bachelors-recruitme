using System;
using System.Collections.Generic;
using System.Text;

namespace RecrutMe.Logic.Operations
{
    public class ValidationFailedException : Exception
    {
        public ValidationResult ValidationResult { get; set; }
    }
}
