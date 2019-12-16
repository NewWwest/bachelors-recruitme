using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EndpointConfig
    {
        public static string BaseAddress => "http://*:5000/";//"http://localhost:5000";//"http://192.168.0.52:5000/";
        
        public static string ConfirmEmail => "/api/account/confirmEmail";
        public static string SetNewPassword => "/account/SetNewPassword";
        public static string EmailVerified(string candidateId) => $"/account/EmailVerified?candidateId={candidateId}";
    }
}
