using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class AddOrUpdateExamTakerCommand : BaseAsyncOperation<int,ExamTakerDto>
    {
        public AddOrUpdateExamTakerCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<int> Execute(ExamTakerDto request)
        {
            EntityEntry<ExamTaker> entity = null;
            if (request.ExamId > 0)
            {
                entity = _dbContext.ExamTakers.Update(request.ToEntity());
            }
            else
            {
                entity = _dbContext.ExamTakers.Add(request.ToEntity());
            }

            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }
    }
}
