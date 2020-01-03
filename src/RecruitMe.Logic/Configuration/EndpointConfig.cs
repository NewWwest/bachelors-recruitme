using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EndpointConfig : IAutoComponent
    {
        public EndpointConfig(BusinessConfiguration businessConfiguration)
        {
            BaseAddress = businessConfiguration.BaseAddress;
        }

        public string BaseAddress { get; private set; }
        public string ConfirmEmail(string guid) => BaseAddress + $"/api/account/confirmEmail/{guid}";
        public string SetNewPassword(string id) => BaseAddress + $"/account/SetNewPassword?token={id}";
        public string EmailVerified(string candidateId) => BaseAddress + $"/account/EmailVerified?candidateId={candidateId}";
        public string AdminPanelDetailsCandidatate(string candidateId) => BaseAddress + $"/adminpanel/details/candidate/{candidateId}";
        public string SuccessfulMoneyTransfer => "/api/payment/successfulMoneyTransfer";
        public string AfterPayment => "/api/payment/afterPayment";

        public string DotpayBaseAddress => "https://ssl.dotpay.pl";
        public string DotpayProductionAddress => DotpayBaseAddress + "/s2/login/api/v1";
        public string DotpayTestAddress => DotpayBaseAddress + "/test_seller/api/v1";
        public string CreatePaymentLink => $"/accounts/{PaymentConfiguration.Id}/payment_links/";
    }
}
