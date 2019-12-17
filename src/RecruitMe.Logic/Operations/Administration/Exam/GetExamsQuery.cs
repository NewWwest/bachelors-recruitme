using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class GetExamsQuery : BaseAsyncOperation<IEnumerable<ExamDto>>
    {
        public GetExamsQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<IEnumerable<ExamDto>> Execute()
        {
            return (
                await _dbContext.Exams
                .Include(e => e.ExamCategory)
                .ToListAsync()
                )
                .Select(e => ExamDto.FromEntity(e, e.ExamCategory.Name));
        }
    }
}
