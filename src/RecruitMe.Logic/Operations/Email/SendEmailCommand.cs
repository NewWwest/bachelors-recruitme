using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Email
{
    public class SendEmailCommand : BaseOperation<OperationResult, EmailDto>
    {
        public SendEmailCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override OperationResult DoExecute(EmailDto request)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("RecruitMeSystem@gmail.com", "Tester123!"),
                EnableSsl = true
            };
            client.Send("RecruitMeSystem@gmail.com", request.To, request.Title, request.Body);
            _logger.Log($"Sent Email to {request.To}");
            return new OperationSucceded();
        }
    }
}
