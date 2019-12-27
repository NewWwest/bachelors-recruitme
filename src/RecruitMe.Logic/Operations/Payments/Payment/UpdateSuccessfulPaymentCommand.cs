using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.Payment
{
    public class UpdateSuccessfulPaymentCommand : BaseAsyncOperation<OperationResult, PaymentResponseDto>
    {
        public UpdateSuccessfulPaymentCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<OperationResult> Execute(PaymentResponseDto response)
        {
            int paymentId = GetPaymentIdFromControl(response.Control);
            Data.Entities.Payment payment = await _dbContext.Payments.FindAsync(paymentId);

            if (payment != null)
            {
                payment.DotpayOperationNumber = response.Number;
                payment.PaidDate = response.OperationDatetime != DateTime.MinValue ? response.OperationDatetime : DateTime.Now;

                _dbContext.Payments.Update(payment);
                await _dbContext.SaveChangesAsync();

                return new OperationSucceded();
            }

            return new OperationFailed();
        }

        private int GetPaymentIdFromControl(string control)
        {
            int index = control.IndexOf(':');
            int length = control.Length;
            string id = control.Substring(index + 1, length - index);

            return int.Parse(id);
        }
    }
}
