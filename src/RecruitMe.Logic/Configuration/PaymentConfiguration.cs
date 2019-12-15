using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class PaymentConfiguration
    {
        public static int Id => 739094;
        public static decimal RegistrationFee => 7.0M;
        public static string Currency => "PLN";

        internal static string TestPIN => "BZBIQwWeul9VUsPavNumCnIDhRujLILZ";
        internal static string ProdPIN => "";

        internal static string AuthToken => "enlza293c2tpZkBzdHVkZW50Lm1pbmkucHcuZWR1LnBsOmRvdHBheTIwMTl4ZA==";
    }
}
