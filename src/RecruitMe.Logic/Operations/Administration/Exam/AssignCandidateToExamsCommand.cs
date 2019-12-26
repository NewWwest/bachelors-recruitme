using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class AssignCandidateToExamsCommand : BaseAsyncOperation<OperationResult, User>
    {
        public AssignCandidateToExamsCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<OperationResult> Execute(User user)
        {
            List<Data.Entities.Exam> exams = await _dbContext.Exams
                .Include(e => e.ExamCategory)
                .Include(e => e.ExamTakers)
                .ToListAsync();

            IEnumerable<Task> tasks = exams.Select(exam => AddExamTaker(exam, user));
            await Task.WhenAll(tasks);

            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }

        private Task<EntityEntry<ExamTaker>> AddExamTaker(Data.Entities.Exam exam, User user)
        {
            DateTime startDate;

            if (exam.ExamCategory.ExamType == ExamType.Collective)
                startDate = exam.StartDateTime;
            else
            {
                int examTakersCount = exam.ExamTakers.Count();
                startDate = exam.StartDateTime.AddMinutes(exam.DurationInMinutes * examTakersCount);
            }

            ExamTaker examTaker = new ExamTaker()
            {
                ExamId = exam.Id,
                UserId = user.Id,
                StartDate = startDate
            };

            return _dbContext.ExamTakers.AddAsync(examTaker);
        }
    }
}
