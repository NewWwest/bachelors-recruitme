using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Administration.Exam;
using RecruitMe.Logic.Operations.Payments.Payment;
using RecruitMe.Logic.Operations.Payments.PaymentLink;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.SuccessfulMoneyTransfer
{
    public class SuccessfulMoneyTransferCommand :
        BaseAsyncOperation<OperationResult, SuccessfulMoneyTransferDto, SuccessfulMoneyTransferParamValidator>
    {
        private readonly UpdateSuccessfulPaymentCommand _updateSuccessfulPaymentCommand;
        private readonly RemoveExistingPaymentLink _removeExistingPaymentLink;
        private readonly AssignCandidateToExamsCommand _assignCandidateToExamsCommand;

        public SuccessfulMoneyTransferCommand(ILogger logger, 
            SuccessfulMoneyTransferParamValidator validator, BaseDbContext dbContext,
            UpdateSuccessfulPaymentCommand updateSuccessfulPaymentCommand,
            RemoveExistingPaymentLink removeExistingPaymentLink,
            AssignCandidateToExamsCommand assignCandidateToExamsCommand) : base(logger, validator, dbContext)
        {
            _assignCandidateToExamsCommand = assignCandidateToExamsCommand;
            _removeExistingPaymentLink = removeExistingPaymentLink;
            _updateSuccessfulPaymentCommand = updateSuccessfulPaymentCommand;
        }

        protected async override Task<OperationResult> DoExecute(SuccessfulMoneyTransferDto transferDto)
        {
            User user = transferDto.User;
            PaymentResponseDto response = transferDto.DotpayResponse;

            //insert successful payment
            await _updateSuccessfulPaymentCommand.Execute(response);

            //delete previously used link
            int rows = await _removeExistingPaymentLink.Execute(user.Id);
            if (rows != 1) throw new Exception($"Deleted different number of rows than one. Actual value: {rows}");

            //auto-assign candidate to all exams
            await _assignCandidateToExamsCommand.Execute(user);

            return new OperationSucceded();
        }
    }
}
