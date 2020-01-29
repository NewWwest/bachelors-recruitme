using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data.Entities;

namespace RecruitMe.Logic.Operations.Messages.Queries
{
    public class GetNewMessagesCountQuery : BaseAsyncOperation<int, User>
    {
        public GetNewMessagesCountQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<int> Execute(User user)
        {
            return await _dbContext.Messages.CountAsync(m => m.ToId == user.Id && !m.IsRead);
        }
    }
}
