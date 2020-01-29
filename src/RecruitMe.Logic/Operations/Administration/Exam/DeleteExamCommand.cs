using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class DeleteExamCommand : BaseAsyncOperation<OperationResult, int>
    {
        public DeleteExamCommand(ILogger logger, BaseDbContext dbContext) : base(logger,  dbContext)
        {
        }

        public override async Task<OperationResult> Execute(int request)
        {
            _dbContext.Exams.Remove(await _dbContext.Exams.FindAsync(request)); 
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
