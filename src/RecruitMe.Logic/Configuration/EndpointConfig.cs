using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EndpointConfig
    {
        public static string BaseAddress => "http://localhost:5000";//"http://192.168.0.52:5000/";
        
        public static string ConfirmEmail => "/api/account/confirmEmail";
        public static string SetNewPassword => "/account/SetNewPassword";
        public static string EmailVerified(string candidateId) => $"/account/EmailVerified?candidateId={candidateId}";
        public static string SuccessfulMoneyTransfer => "/api/payment/successfulMoneyTransfer";

        private static string DotpayBaseAddress => "https://ssl.dotpay.pl";
        public static string DotpayProductionAddress => DotpayBaseAddress + "/s2/login/api/v1";
        public static string DotpayTestBaseAddress => DotpayBaseAddress + "/test_seller/api/v1";
        public static string CreatePaymentLink => $"/accounts/{PaymentConfiguration.Id}/payment_links/";
    }
}
