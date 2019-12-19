using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Teacher
{
    public class UpdateTeacherCommand : BaseAsyncOperation<OperationResult, TeacherDto, UpdateTeacherValidator>
    {
        public UpdateTeacherCommand(ILogger logger, UpdateTeacherValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(TeacherDto request)
        {
            _dbContext.Teachers.Update(request.ToEntity());
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
