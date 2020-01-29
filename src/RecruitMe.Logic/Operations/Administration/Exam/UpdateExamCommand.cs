using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class UpdateExamCommand : BaseAsyncOperation<OperationResult, ExamDto, UpdateExamValidator>
    {
        public UpdateExamCommand(ILogger logger, UpdateExamValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(ExamDto request)
        {
            _dbContext.Exams.Update(request.ToEntity());
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
