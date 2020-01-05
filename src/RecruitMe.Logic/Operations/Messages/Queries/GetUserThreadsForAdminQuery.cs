using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Messages.Queries
{
    public class GetUserThreadsForAdminQuery : BaseAsyncOperation<IEnumerable<UserThreadDto>, User>
    {
        public GetUserThreadsForAdminQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<IEnumerable<UserThreadDto>> Execute(User admin)
        {
            var query = await _dbContext.Messages.Where(m => m.ToId == admin.Id)
                                .Include(m => m.From)
                                .GroupBy(m => m.FromId)
                                .Select(g => new
                                {
                                    UserId = g.Key,
                                    Count = g.Count(m => m.IsRead),
                                    From = g.Select(m => m.From).FirstOrDefault()
                                })
                                .ToListAsync();

            return query.Select(q => new UserThreadDto()
            {
                UserId = q.UserId,
                NewMessagesCount = q.Count,
                DisplayName = $"{q.From?.Name} {q.From?.Surname} ({q.From?.CandidateId})"
            });
        }
    }
}
