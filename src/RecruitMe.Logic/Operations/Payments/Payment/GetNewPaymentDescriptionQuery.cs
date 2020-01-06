using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.Payment
{
    public class GetNewPaymentDescriptionQuery : BaseAsyncOperation<string, int>
    {
        public GetNewPaymentDescriptionQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {

        }

        public async override Task<string> Execute(int userId)
        {
            int year = DateTime.Today.Year;
            int count = await _dbContext.Payments.CountAsync(p => p.IssueDate.Year == year);
            int id = userId;

            return $"Opłata rekrutacyjna {id}/{count+1}/{year}";
        }
    }
}
