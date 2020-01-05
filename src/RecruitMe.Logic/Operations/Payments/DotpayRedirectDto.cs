using Newtonsoft.Json;
using RecruitMe.Logic.Operations.Payments.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments
{
    public class DotpayRedirectDto
    {
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("error_code")]
        public ErrorCode ErrorCode { get; set; }
    }
}
