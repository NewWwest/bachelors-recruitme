using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class EndpointConfig
    {
        public static string BaseAddress => "http://localhost:5000";
        
        public static string ConfirmEmail => "/api/account/confirmEmail";
        public static string SetNewPassword => "/account/SetNewPassword";
    }
}
