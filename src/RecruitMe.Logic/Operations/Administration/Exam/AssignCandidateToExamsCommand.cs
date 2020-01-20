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
    public class AssignCandidateToExamsCommand : BaseAsyncOperation<OperationResult, int>
    {
        public AssignCandidateToExamsCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<OperationResult> Execute(int userId)
        {
            var alreadyAssigned = await _dbContext.ExamTakers.AnyAsync(et => et.UserId == userId);
            if (alreadyAssigned)
            {
                throw new ValidationFailedException()
                {
                    ValidationResult = new ValidationResult($"Kandydat o Id:{userId} jest już zapisany na jakiś egzamin i nie może być zapisany automatycznie")
                };
            }

            List<Data.Entities.Exam> exams = await _dbContext.Exams
                .Include(e => e.ExamCategory)
                .Include(e => e.ExamTakers)
                .Where(e => e.SeatCount > e.ExamTakers.Count())
                .GroupBy(e => e.ExamCategoryId)
                .Select(e => e.OrderBy(e2 => e2.StartDateTime).FirstOrDefault())
                .ToListAsync();

            foreach (var exam in exams)
            {
                AddExamTaker(exam, userId);
            }

            await _dbContext.SaveChangesAsync();
            _logger.Log($"Kandydat o Id:{userId} został zapisany na {exams.Count} egzaminów ({string.Join(',', exams.Select(e => e.Id.ToString()))})");
            return new OperationSucceded();
        }

        private void AddExamTaker(Data.Entities.Exam exam, int userId)
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
                UserId = userId,
                StartDate = startDate
            };

            _dbContext.ExamTakers.Add(examTaker);
        }
    }
}
