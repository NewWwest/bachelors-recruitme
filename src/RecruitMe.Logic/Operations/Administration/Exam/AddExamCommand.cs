using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class AddExamCommand : BaseAsyncOperation<OperationResult, ExamDto, AddExamValidator>
    {
        public AddExamCommand(ILogger logger, AddExamValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(ExamDto request)
        {
            _dbContext.Exams.Add(request.ToEntity());
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
