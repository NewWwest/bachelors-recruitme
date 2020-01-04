using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class PaymentConfiguration
    {
        public int Id { get; set; }
        public decimal RegistrationFee { get; set; }
        public string Currency { get; set; }
        public string PIN { get; set; }
        public string AuthToken { get; set; }
        public bool UseProductionServer { get; set; }
    }
}
