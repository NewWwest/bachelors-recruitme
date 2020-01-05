using FluentValidation;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Payments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.SuccessfulMoneyTransfer
{
    public class SuccessfulMoneyTransferParamValidator : BaseValidator<PaymentResponseDto>
    {
        public SuccessfulMoneyTransferParamValidator()
        {
            RuleFor(a => a.Type).Equal(OperationType.Payment);
            RuleFor(a => a.Status).Equal(OperationStatus.Completed);
            RuleFor(a => a.OperationAmount)
                .Equal(p => p.OperationOriginalAmount);
            RuleFor(a => a.OperationCurrency)
                .Equal(p => p.OperationOriginalCurrency);
            // should check for signature also, but have no assurance that all properties in
            // PaymentResponseDto were hashed in response.Signature
            // so we check what we can
        }
    }
}
