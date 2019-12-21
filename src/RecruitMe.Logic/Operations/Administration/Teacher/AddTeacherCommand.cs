using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Teacher
{
    public class AddTeacherCommand : BaseAsyncOperation<OperationResult, TeacherDto, AddTeacherValidator>
    {
        public AddTeacherCommand(ILogger logger, AddTeacherValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(TeacherDto request)
        {
            _dbContext.Teachers.Add(request.ToEntity());
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
