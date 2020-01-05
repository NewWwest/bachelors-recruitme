using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Operations.Payments.PaymentLink;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RecruitMe.Logic.Operations.Payments.Helpers
{
    public class RequestHasher
    {
        public static string GetControlChecksum(PaymentLinkResponse response, PaymentConfiguration paymentConfiguration)
        {
            using (SHA256 hasher = SHA256.Create())
            {
                string neededParams = paymentConfiguration.PIN + response.Token;

                byte[] hashedBytes = hasher.ComputeHash(Encoding.ASCII.GetBytes(neededParams));

                StringBuilder sb = new StringBuilder();
                foreach (byte @byte in hashedBytes)
                {
                    sb.Append(@byte.ToString("x2"));    
                }

                return sb.ToString();
            }
        }
    }
}
