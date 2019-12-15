using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.PaymentLink
{
    public class PaymentLinkResponse : PaymentDto
    {
        public string Href { get; set; }
        public string Payment_Url { get; set; }
        public string Token { get; set; }
    }
}
