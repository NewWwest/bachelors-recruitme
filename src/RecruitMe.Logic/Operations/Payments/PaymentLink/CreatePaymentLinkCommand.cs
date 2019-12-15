using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Payments.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.PaymentLink
{
    public class CreatePaymentLinkCommand : BaseAsyncOperation<string, PaymentDto>
    {
        public CreatePaymentLinkCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override async Task<string> DoExecute(PaymentDto request)
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = "";
                #if DEBUG
                    requestUrl = EndpointConfig.DotpayTestBaseAddress;
                #else
                    requestUrl = EndpointConfig.DotpayProductionAddress;
                #endif
                requestUrl += EndpointConfig.CreatePaymentLink;

                //client.SetBasicAuthentication(PaymentConfiguration.Login, PaymentConfiguration.Password);
                client.SetToken("Basic", "enlza293c2tpZkBzdHVkZW50Lm1pbmkucHcuZWR1LnBsOmRvdHBheTIwMTl4ZA==");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(requestUrl, request);
                if (response.IsSuccessStatusCode)
                {
                    PaymentLinkResponse linkResponse = await response.Content.ReadAsAsync<PaymentLinkResponse>();
                    string chk = RequestHasher.GetControlChecksum(linkResponse);

                    string redirectUrl =
                        requestUrl.Substring(0, requestUrl.Length - EndpointConfig.CreatePaymentLink.Length) +
                        $"?chk={chk}&pid={linkResponse.Token}";

                    return redirectUrl;
                }

                throw new Exception(response.ToString());
            }
        }
    }
}
