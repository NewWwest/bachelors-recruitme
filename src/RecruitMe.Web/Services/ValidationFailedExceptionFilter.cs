using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Services
{
    public class ValidationFailedExceptionFilter : IExceptionFilter
    {

        public ValidationFailedExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            if(context.Exception is ValidationFailedException exc)
            {
                context.Result = new BadRequestObjectResult(exc.ValidationResult.Errors);
            }
        }
    }
}

