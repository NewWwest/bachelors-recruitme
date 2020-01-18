using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System.Net;
using System.Net.Mail;

namespace RecruitMe.Logic.Operations.Email
{
    public class SendEmailCommand : BaseOperation<OperationResult, EmailDto>
    {
        private readonly BusinessConfiguration _businessConfiguration;

        public SendEmailCommand(ILogger logger, BaseDbContext dbContext, BusinessConfiguration businessConfiguration) : base(logger, dbContext)
        {
            _businessConfiguration = businessConfiguration;
        }

        public override OperationResult Execute(EmailDto request)
        {
            using (SmtpClient client = new SmtpClient(_businessConfiguration.EmailServer, _businessConfiguration.EmailServerPort)
            {
                Credentials = new NetworkCredential(_businessConfiguration.Email, _businessConfiguration.EmailPassword),
                EnableSsl = true
            })
            {
                client.Send(_businessConfiguration.Email, request.To, request.Title, request.Body);
                _logger.Log($"Sent Email to {request.To}");
                return new OperationSucceded();
            }
        }
    }
}
