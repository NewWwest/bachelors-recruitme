using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;

namespace RecruitMe.Logic.Operations.Recruitment.MyRecruitment
{
    public class GetExamsAndStatusQuery : BaseAsyncOperation<ExamsAndStatusDto, int>
    {
        public GetExamsAndStatusQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<ExamsAndStatusDto> Execute(int request)
        {
            var result = new ExamsAndStatusDto()
            {
                Status = GetRecrutationStatus(request),
                Exams = await GetExams(request)
            };

            return result;
        }

        private async Task<List<ExamDataDto>> GetExams(int request)
        {
            var data = await _dbContext.ExamTakers
                .Include(et => et.Exam)
                .ThenInclude(e => e.ExamCategory)
                .Where(et => et.UserId == request)
                .Select(et => new
                {
                    et.StartDate,
                    et.Exam.DurationInMinutes,
                    et.Exam.ExamCategory.Name,
                    et.Exam.ExamCategory.ExamType
                })
                .ToListAsync();

            return data.Select(e => new ExamDataDto()
            {
                CategoryName = e.Name,
                DurationInMinutes = e.DurationInMinutes,
                ExamType = e.ExamType,
                StartTime = e.StartDate
            }).ToList();
        }

        private RecrutationStatus GetRecrutationStatus(int request)
        {
#warning TODO: after merge with payments implement this method
            return RecrutationStatus.Registration;
        }
    }
}
