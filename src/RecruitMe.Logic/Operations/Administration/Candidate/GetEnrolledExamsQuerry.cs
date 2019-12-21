using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
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
    public class GetEnrolledExamsQuerry : BaseAsyncOperation<List<ExamTakerDto>, int>
    {
        public GetEnrolledExamsQuerry(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<List<ExamTakerDto>> Execute(int request)
        {
            var user = await _dbContext.Users.SingleAsync(u => u.Id == request);
            var exams = await _dbContext.ExamTakers
                .Include(et => et.Teacher)
                .Where(et => et.UserId == request)
                .Select(et => new { taker = et, category = et.Exam.ExamCategory.Name })
                .ToListAsync();

            return exams.Select(etc => ExamTakerDto.FromEntities(etc.taker, user, etc.taker.Teacher, etc.category)).ToList();
        }
    }
}
