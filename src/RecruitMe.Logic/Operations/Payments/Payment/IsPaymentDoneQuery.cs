using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.Payment
{
    public class IsPaymentDoneQuery : BaseAsyncOperation<bool, User>
    {
        public IsPaymentDoneQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<bool> Execute(User user)
        {
            Data.Entities.Payment payment = await _dbContext.Payments.FindAsync(user.Id);

            return payment != null && payment.PaidDate.HasValue && (payment.PaidDate.Value > DateTime.MinValue);
        }
    }
}
