using RecrutMe.Logic.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecrutMe.Logic.Operations
{
    public abstract class BaseOperation
    {
        protected readonly ILogger _logger;

        public BaseOperation(ILogger logger)
        {
            _logger = logger;
        }
    }

    public abstract class BaseOperation<TResult, TRequest> : BaseOperation
    {
        public BaseOperation(ILogger logger) : base(logger)
        {
        }

        public TResult Execute(TRequest request)
        {
            return DoExecute(request);
        }

        protected abstract TResult DoExecute(TRequest request);
    }

    public abstract class BaseAsyncOperation<TResult, TRequest> : BaseOperation
    {
        public BaseAsyncOperation(ILogger logger) : base(logger)
        {
        }

        public Task<TResult> Execute(TRequest request)
        {
            return DoExecute(request);
        }

        protected abstract Task<TResult> DoExecute(TRequest request);
    }

    public abstract class BaseOperation<TResult, TRequest, TValidator> : BaseOperation where TValidator : BaseValidator<TRequest>
    {
        private readonly TValidator _validator;

        public BaseOperation(ILogger logger, TValidator validator) : base(logger)
        {
            _validator = validator;
        }

        public TResult Execute(TRequest request)
        {
            var validationResult = _validator.ValidateRequest(request);
            if (validationResult.Success)
            {
                return DoExecute(request);
            }
            else
            {
                _logger.Log(string.Join(' ', validationResult.Errors));
                throw new ValidationFailedException() { ValidationResult = validationResult };
            }
        }

        protected abstract TResult DoExecute(TRequest request);
    }

    public abstract class BaseAsyncOperation<TResult, TRequest, TValidator> : BaseOperation where TValidator : BaseValidator<TRequest>
    {
        private readonly TValidator _validator;

        public BaseAsyncOperation(ILogger logger, TValidator validator) : base(logger)
        {
            _validator = validator;
        }

        public Task<TResult> Execute(TRequest request)
        {
            var validationResult = _validator.ValidateRequest(request);
            if (validationResult.Success)
            {
                return DoExecute(request);
            }
            else
            {
                _logger.Log(string.Join(' ', validationResult.Errors));
                throw new ValidationFailedException() { ValidationResult = validationResult };
            }
        }

        protected abstract Task<TResult> DoExecute(TRequest request);
    }
}
