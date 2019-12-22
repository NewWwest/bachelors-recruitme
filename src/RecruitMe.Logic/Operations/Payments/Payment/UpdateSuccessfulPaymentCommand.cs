using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.Payment
{
    public class UpdateSuccessfulPaymentCommand : BaseAsyncOperation<int, PaymentResponseDto>
    {
        public UpdateSuccessfulPaymentCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override Task<int> Execute(PaymentResponseDto request)
        {
            throw new NotImplementedException();
        }
    }
}
