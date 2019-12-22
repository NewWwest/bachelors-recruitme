using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.PaymentLink
{
    public class RemoveExistingPaymentLink : BaseAsyncOperation<int, int>
    {
        public RemoveExistingPaymentLink(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override Task<int> Execute(int id)
        {
            Data.Entities.PaymentLink paymentLink = _dbContext.PaymentLinks.Where(p => p.UserId == id).FirstOrDefault();
            _dbContext.PaymentLinks.Remove(paymentLink);

            return _dbContext.SaveChangesAsync();
        }
    }
}
