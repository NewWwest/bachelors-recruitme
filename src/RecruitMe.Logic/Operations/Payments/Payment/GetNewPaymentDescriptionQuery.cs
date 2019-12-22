using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.Payment
{
    public class GetNewPaymentDescriptionQuery : BaseOperation<string, int>
    {
        public GetNewPaymentDescriptionQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {

        }

        public override string Execute(int userId)
        {
            int year = DateTime.Today.Year;
            int count = _dbContext.Payments.Count(p => p.IssueDate.Year == year);
            int id = userId;

            return $"Opłata rekrutacyjna {id}/{count+1}/{year}";
        }
    }
}
