using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Abstractions
{
    public abstract class BaseOperation
    {
        protected readonly ILogger _logger;
        protected readonly BaseDbContext _dbContext;

        public BaseOperation(ILogger logger, BaseDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }

    public abstract class BaseOperation<TResult, TRequest> : BaseOperation
    {
        public BaseOperation(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
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
        public BaseAsyncOperation(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
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

        public BaseOperation(ILogger logger, TValidator validator, BaseDbContext dbContext) : base(logger, dbContext)
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

        public BaseAsyncOperation(ILogger logger, TValidator validator, BaseDbContext dbContext) : base(logger, dbContext)
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
