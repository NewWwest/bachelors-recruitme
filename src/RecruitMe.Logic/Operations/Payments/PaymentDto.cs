using RecruitMe.Logic.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments
{
    public class PaymentDto
    {
        public int Id => PaymentConfiguration.Id;
        public decimal Amount => PaymentConfiguration.RegistrationFee;
        public string Currency => PaymentConfiguration.Currency;
        public string Description { get; set; }
        public string Control { get; set; }
        public string Language => "pl";
        public int Ignore_Last_Payment_Channel => 1;
        public int Redirection_Type => 0;
        public string Url { get; private set; }
        public string Urlc { get; private set; }
        public PayerDto Payer { get; private set; }

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
