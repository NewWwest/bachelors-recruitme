using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.PaymentLink
{
    public class GetExistingPaymentLinkQuery : BaseOperation<string, int>
    {
        public GetExistingPaymentLinkQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override string DoExecute(int userId) => _dbContext.PaymentLinks.Where(p => p.UserId == userId).FirstOrDefault().Link;
    }
}
