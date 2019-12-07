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
    public class GetExamDetailsQuery : BaseAsyncOperation<ExamDetailsDto, int>
    {
        public GetExamDetailsQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<ExamDetailsDto> Execute(int request)
        {
            var exam = await _dbContext.Exams
                .Include(e => e.ExamTakers)
                .ThenInclude(et => et.User)
                .Include(e => e.ExamTakers)
                .ThenInclude(et => et.Teacher)
                .SingleAsync(e => e.Id == request);

            var examdto = new ExamDetailsDto()
            {
                Exam = ExamDto.FromEntity(exam),
                ExamTakers = exam.ExamTakers.Select(et => ExamTakerDto.FromEntities(et, et.User, et.Teacher)).ToList()
            };
            return examdto;
        }
    }
}
