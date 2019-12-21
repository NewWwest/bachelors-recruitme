using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class DeleteExamTakerCommand : BaseAsyncOperation<OperationResult, int>
    {
        public DeleteExamTakerCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<OperationResult> Execute(int request)
        {
            _dbContext.ExamTakers.Remove(await _dbContext.ExamTakers.SingleAsync(et => et.Id == request));

            await _dbContext.SaveChangesAsync();

            return new OperationSucceded();
        }
    }
}
