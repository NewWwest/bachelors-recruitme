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
    public class RemoveExistingPaymentLink : BaseAsyncOperation<OperationResult, int>
    {
        public RemoveExistingPaymentLink(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<OperationResult> Execute(int id)
        {
            Data.Entities.PaymentLink paymentLink = _dbContext.PaymentLinks.Where(p => p.UserId == id).FirstOrDefault();
            _dbContext.PaymentLinks.Remove(paymentLink);

            int rows = await _dbContext.SaveChangesAsync();
            if (rows != 1) throw new Exception($"Deleted different number of rows than one. Actual value: {rows}");

            return new OperationSucceded();
        }
    }
}
