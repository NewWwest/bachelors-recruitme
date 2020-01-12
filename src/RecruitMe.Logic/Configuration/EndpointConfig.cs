using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EndpointConfig : IAutoComponent
    {
        private int _dotpaySellerId;
        private bool _dotpayUseProductionDatabase;

        public EndpointConfig(BusinessConfiguration businessConfiguration, PaymentConfiguration paymentConfiguration)
        {
            BaseAddress = businessConfiguration.BaseAddress;

            _dotpaySellerId = paymentConfiguration.Id;
            _dotpayUseProductionDatabase = paymentConfiguration.UseProductionServer;
        }

        public string BaseAddress { get; private set; }
        public string ConfirmEmail(string guid) => BaseAddress + $"/api/account/confirmEmail/{guid}";
        public string SetNewPassword(string id) => BaseAddress + $"/account/SetNewPassword?token={id}";
        public string EmailVerified(string candidateId) => BaseAddress + $"/account/EmailVerified?candidateId={candidateId}";
        public string AdminPanelDetailsCandidatate(string candidateId) => BaseAddress + $"/adminpanel/details/candidate/{candidateId}";
        public string PaymentsThankYou(string err) => string.IsNullOrEmpty(err) ?
            BaseAddress + "/payments/thankyou" : BaseAddress + $"/payments/thankyou?error={err}";
        public string PaymentsDeleteLink(string token) => _dotpayUseProductionDatabase ?
            DotpayProductionAddress + $"/accounts/{_dotpaySellerId}/payment_links/{token}" :
            DotpayTestAddress + $"/accounts/{_dotpaySellerId}/payment_links/{token}";
        public string SuccessfulMoneyTransfer => "/api/payment/successfulMoneyTransfer";
        public string AfterPayment => "/api/payment/afterPayment";

        public string DotpayBaseAddress => "https://ssl.dotpay.pl";
        public string DotpayProductionAddress => DotpayBaseAddress + "/s2/login/api/v1";
        public string DotpayTestAddress => DotpayBaseAddress + "/test_seller/api/v1";
        public string CreatePaymentLink => $"/accounts/{_dotpaySellerId}/payment_links/";
    }
}
