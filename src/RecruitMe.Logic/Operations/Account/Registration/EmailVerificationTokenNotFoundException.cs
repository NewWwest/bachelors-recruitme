using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Registration
{
    public class EmailVerificationTokenNotFoundException : Exception
    {
        public EmailVerificationTokenNotFoundException()
        {
        }

        public EmailVerificationTokenNotFoundException(string message) : base(message)
        {
        }

        public EmailVerificationTokenNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailVerificationTokenNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
