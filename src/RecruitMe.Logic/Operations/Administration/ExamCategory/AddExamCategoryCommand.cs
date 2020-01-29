using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.ExamCategory
{
    public class AddExamCategoryCommand : BaseAsyncOperation<OperationResult, ExamCategoryDto, AddExamCategoryValidator>
    {
        public AddExamCategoryCommand(ILogger logger, AddExamCategoryValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(ExamCategoryDto request)
        {
            _dbContext.ExamCategories.Add(request.ToEntity());
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
