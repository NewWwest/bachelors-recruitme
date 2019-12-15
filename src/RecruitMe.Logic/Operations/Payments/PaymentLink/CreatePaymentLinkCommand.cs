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
using Newtonsoft.Json;

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
                string dataUrl = "";
#if DEBUG
                dataUrl = EndpointConfig.DotpayTestAddress;
#else
                dataUrl = EndpointConfig.DotpayProductionAddress;
#endif

                client.BaseAddress = new Uri(EndpointConfig.DotpayBaseAddress);
                client.SetToken("Basic", PaymentConfiguration.AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post,
                    dataUrl + EndpointConfig.CreatePaymentLink);
                httpRequest.Content = GetRequestContent(request);

                HttpResponseMessage response = await client.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    PaymentLinkResponse linkResponse = await response.Content.ReadAsAsync<PaymentLinkResponse>();
                    string chk = RequestHasher.GetControlChecksum(linkResponse);

                    return linkResponse.Payment_Url + $"&chk={chk}";
                }

                throw new Exception(response.ToString());
            }
        }

        private HttpContent GetRequestContent(PaymentDto request)
        {
            string userId = GetUserIdFromDscription(request.Description);

            var obj = new
            {
                id = request.Id,
                amount = request.Amount,
                currency = request.Currency,
                description = request.Description,
                control = userId,
                language = request.Language,
                ignore_last_payment_channel = request.Ignore_Last_Payment_Channel,
                url = request.Url,
                urlc = request.Urlc,
            };

            string content = JsonConvert.SerializeObject(obj);

            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private string GetUserIdFromDscription(string description)
        {
            // "Opłata rekrutacyjna <userId>/<paymentId>/<year>

            int i1 = description.LastIndexOf(' ');
            int i2 = description.IndexOf('/');

            return description.Substring(i1 + 1, i2 - i1 - 1);
        }
    }
}
