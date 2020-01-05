using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.SuccessfulMoneyTransfer
{
    public class SuccessfulMoneyTransferDto
    {
        public User User { get; set; }
        public PaymentResponseDto DotpayResponse { get; set; }
    }
}
