using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Payments.PaymentLink
{
    public class RemovePaymentLinkInDotpayCommand : BaseAsyncOperation<OperationResult, Data.Entities.PaymentLink>
    {
        private readonly EndpointConfig _endpointConfig;
        private readonly PaymentConfiguration _paymentConfiguration;

        public RemovePaymentLinkInDotpayCommand(ILogger logger, BaseDbContext dbContext,
            EndpointConfig endpointConfig,
            PaymentConfiguration paymentConfiguration) : base(logger, dbContext)
        {
            _endpointConfig = endpointConfig;
            _paymentConfiguration = paymentConfiguration;
        }

        public async override Task<OperationResult> Execute(Data.Entities.PaymentLink link)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endpointConfig.DotpayBaseAddress);
                client.SetToken("Basic", _paymentConfiguration.AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, 
                    _endpointConfig.PaymentsDeleteLink(link.Token));

                HttpResponseMessage response = await client.SendAsync(httpRequest);

                return response.IsSuccessStatusCode ? new OperationSucceded() : (OperationResult)new OperationFailed(); 
            }
        }
    }
}
