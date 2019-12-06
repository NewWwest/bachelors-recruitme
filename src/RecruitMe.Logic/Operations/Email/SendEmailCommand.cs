using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System.Net;
using System.Net.Mail;

namespace RecruitMe.Logic.Operations.Email
{
    public class SendEmailCommand : BaseOperation<OperationResult, EmailDto>
    {

        public SendEmailCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override OperationResult Execute(EmailDto request)
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
