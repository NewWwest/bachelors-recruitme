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
    public class InsertNewPaymentCommand : BaseAsyncOperation<Data.Entities.Payment, User>
    {
        GetNewPaymentDescriptionQuery _getNewPaymentDescriptionQuery;

        public InsertNewPaymentCommand(ILogger logger, BaseDbContext dbContext,
            GetNewPaymentDescriptionQuery getNewPaymentDescriptionQuery) : base(logger, dbContext)
        {
            _getNewPaymentDescriptionQuery = getNewPaymentDescriptionQuery;
        }

        public override async Task<Data.Entities.Payment> Execute(User user)
        {
            Data.Entities.Payment payment = _dbContext.Payments.Where(p => p.UserId == user.Id).FirstOrDefault();

            if (payment == null)
            {
                payment = new Data.Entities.Payment()
                {
                    Description = _getNewPaymentDescriptionQuery.Execute(user.Id),
                    IssueDate = DateTime.Now,
                    UserId = user.Id,
                };

                _dbContext.Payments.Add(payment);
                await _dbContext.SaveChangesAsync();
            }

            return payment;
        }
    }
}
