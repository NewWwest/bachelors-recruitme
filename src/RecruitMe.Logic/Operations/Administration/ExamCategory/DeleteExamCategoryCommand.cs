using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.ExamCategory
{
    public class DeleteExamCategoryCommand : BaseAsyncOperation<OperationResult, int>
    {
        public DeleteExamCategoryCommand(ILogger logger, BaseDbContext dbContext) : base(logger,  dbContext)
        {
        }

        public override async Task<OperationResult> Execute(int request)
        {
            _dbContext.ExamCategories.Remove(await _dbContext.ExamCategories.FindAsync(request));
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
