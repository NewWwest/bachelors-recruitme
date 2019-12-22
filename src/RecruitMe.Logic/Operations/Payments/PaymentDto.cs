using Newtonsoft.Json;
using RecruitMe.Logic.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments
{
    public class PaymentDto
    {
        [JsonProperty("id")]
        public int Id => PaymentConfiguration.Id;
        
        [JsonProperty("amount")]
        public decimal Amount => PaymentConfiguration.RegistrationFee;
        
        [JsonProperty("currency")]
        public string Currency => PaymentConfiguration.Currency;
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("control")]
        public string Control { get; set; }
        
        [JsonProperty("language")]
        public string Language => "pl";
        
        [JsonProperty("ignore_last_payment_channel")]
        public bool IgnoreLastPaymentChannel => true;
        
        [JsonProperty("type")]
        public int Type => 0;
        
        [JsonProperty("buttontext")]
        public string ButtonText => "Wróć do RecruitMe";
        
        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("urlc")]
        public string Urlc { get; private set; }

        [JsonProperty("p_info")]
        public string DisplayName => "Nazwa szkoły";

        [JsonProperty("p_email")]
        public string DisplayEmail => "aa@aa.aa";
        
        [JsonProperty("payer")]
        public PayerDto Payer { get; private set; }

        [JsonProperty("api_version")]
        public string ApiVersion => "dev";

        public void SetPayerAndUrls(PayerDto payerDto, EndpointConfig endpointConfig)
        {
            Payer = payerDto;
            Url = endpointConfig.BaseAddress + endpointConfig.AfterPayment;
            Urlc = endpointConfig.BaseAddress + endpointConfig.SuccessfulMoneyTransfer;
        }
    }

    public class PayerDto
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string Building_Number { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
