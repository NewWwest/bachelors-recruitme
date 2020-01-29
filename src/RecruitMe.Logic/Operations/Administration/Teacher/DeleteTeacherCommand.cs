using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Teacher
{
    public class DeleteTeacherCommand : BaseAsyncOperation<OperationResult, int>
    {
        public DeleteTeacherCommand(ILogger logger, BaseDbContext dbContext) : base(logger,  dbContext)
        {
        }

        public override async Task<OperationResult> Execute(int request)
        {
            _dbContext.Teachers.Remove(await _dbContext.Teachers.FindAsync(request));
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
