using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Utilities.Dates;
using System;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.Payment
{
    public class InsertNewPaymentCommand : BaseAsyncOperation<Data.Entities.Payment, User>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly GetNewPaymentDescriptionQuery _getNewPaymentDescriptionQuery;

        public InsertNewPaymentCommand(ILogger logger, BaseDbContext dbContext,
            GetNewPaymentDescriptionQuery getNewPaymentDescriptionQuery,
            IDateTimeProvider dateTimeProvider) : base(logger, dbContext)
        {
            _dateTimeProvider = dateTimeProvider;
            _getNewPaymentDescriptionQuery = getNewPaymentDescriptionQuery;
        }

        public override async Task<Data.Entities.Payment> Execute(User user)
        {
            Data.Entities.Payment payment = await _dbContext.Payments.FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (payment == null)
            {
                payment = new Data.Entities.Payment()
                {
                    Description = await _getNewPaymentDescriptionQuery.Execute(user.Id),
                    IssueDate = _dateTimeProvider.Now,
                    UserId = user.Id,
                };

                _dbContext.Payments.Add(payment);
                await _dbContext.SaveChangesAsync();
            }

            return payment;
        }
    }
}
