using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Payments.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RecruitMe.Logic.Operations.Payments.PaymentLink
{
    public class CreatePaymentLinkCommand : BaseAsyncOperation<string, PaymentDto>
    {
        private readonly EndpointConfig _endpointConfig;
        private readonly PaymentConfiguration _paymentConfiguration;
        private readonly GetExistingPaymentLinkQuery _getExistingPaymentLinkQuery;

        public CreatePaymentLinkCommand(ILogger logger, BaseDbContext dbContext, EndpointConfig endpointConfig,
            GetExistingPaymentLinkQuery getExistingPaymentLinkQuery,
            PaymentConfiguration paymentConfiguration) : base(logger, dbContext)
        {
            _endpointConfig = endpointConfig;
            _paymentConfiguration = paymentConfiguration;
            _getExistingPaymentLinkQuery = getExistingPaymentLinkQuery;
        }

        public override async Task<string> Execute(PaymentDto request)
        {
            int userId = int.Parse(GetUserIdFromDscription(request.Description));
            string paymentLink = _getExistingPaymentLinkQuery.Execute(userId);
            
            if (string.IsNullOrEmpty(paymentLink))
            {
                paymentLink = await CreatePaymentLinkFromDotpay(request);

                // insert link to db
                _dbContext.PaymentLinks.Add(new Data.Entities.PaymentLink()
                {
                    Link = paymentLink,
                    UserId = userId
                });

                await _dbContext.SaveChangesAsync();
            }

            return paymentLink;
        }

        private async Task<string> CreatePaymentLinkFromDotpay(PaymentDto request)
        {
            using (HttpClient client = new HttpClient())
            {
                string dataUrl = "";
                if (_paymentConfiguration.UseProductionServer)
                    dataUrl = _endpointConfig.DotpayProductionAddress;
                else dataUrl = _endpointConfig.DotpayTestAddress;

                client.BaseAddress = new Uri(_endpointConfig.DotpayBaseAddress);
                client.SetToken("Basic", _paymentConfiguration.AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post,
                    dataUrl + _endpointConfig.CreatePaymentLink);
                httpRequest.Content = GetRequestContent(request);

                HttpResponseMessage response = await client.SendAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    string respString = await response.Content.ReadAsStringAsync();
                    PaymentLinkResponse linkResponse = JsonConvert.DeserializeObject<PaymentLinkResponse>(respString);
                    string chk = RequestHasher.GetControlChecksum(linkResponse, _paymentConfiguration);

                    return linkResponse.Payment_Url + $"&chk={chk}";
                }

                throw new Exception(response.ToString());
            }
        }

        private HttpContent GetRequestContent(PaymentDto request)
        {
            string content = JsonConvert.SerializeObject(request);

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
