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
    public class GetUserThreadsQuery : BaseAsyncOperation<IEnumerable<UserThreadDto>, User>
    {
        public GetUserThreadsQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<IEnumerable<UserThreadDto>> Execute(User user)
        {
            var query = await _dbContext.Messages.Where(m => m.ToId == user.Id)
                                .Include(m => m.From)
                                .GroupBy(m => m.FromId)
                                .Select(g => new
                                {
                                    UserId = g.Key,
                                    Count = g.Count(m => m.IsRead),
                                    Firstname = g.Select(m => m.From.Name).FirstOrDefault(),
                                    Surname = g.Select(m => m.From.Surname).FirstOrDefault(),
                                    CandidateId = g.Select(m => m.From.CandidateId).FirstOrDefault()
                                })
                                .ToListAsync();

            return query.Select(q => new UserThreadDto()
            {
                UserId = q.UserId,
                NewMessagesCount = q.Count,
                DisplayName = $"{q.Firstname} {q.Surname} ({q.CandidateId})"
            });
        }
    }
}
