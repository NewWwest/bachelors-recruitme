using Newtonsoft.Json;
using RecruitMe.Logic.Operations.Payments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments
{
    public class DotpayRedirectDto
    {
        public string status { get; set; }

        public ErrorCode error_code { get; set; }
    }
}
