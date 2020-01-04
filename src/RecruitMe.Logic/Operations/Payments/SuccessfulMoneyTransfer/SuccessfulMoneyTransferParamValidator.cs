using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Payments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.SuccessfulMoneyTransfer
{
    public class SuccessfulMoneyTransferParamValidator : BaseValidator<SuccessfulMoneyTransferDto>
    {
        public SuccessfulMoneyTransferParamValidator()
        {
            RuleFor(a => a.User).NotNull();
            RuleFor(a => a.DotpayResponse).NotNull();

            RuleFor(a => a.DotpayResponse.Type).Equal(OperationType.Payment);
            RuleFor(a => a.DotpayResponse.Status).Equal(OperationStatus.Completed);
            RuleFor(a => a.DotpayResponse.OperationAmount)
                .Equal(p => p.DotpayResponse.OperationOriginalAmount);
            RuleFor(a => a.DotpayResponse.OperationCurrency)
                .Equal(p => p.DotpayResponse.OperationOriginalCurrency);
            // should check for signature also, but have no assurance that all properties in
            // PaymentResponseDto were hashed in response.Signature
            // so we check what we can
        }
    }
}
