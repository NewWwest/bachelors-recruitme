using RecruitMe.Logic.Utilities.Dates;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Tests.UnitTests.Operations.Payments.SuccessfulMoneyTransfer
{
    public class FakeDateProvider : IDateTimeProvider
    {
        public DateTime Now => new DateTime(2001, 1, 1);
    }
}
