using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Email;
using System;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.ResetPassword
{
    public class ResetPasswordCommand : BaseAsyncOperation<OperationResult, ResetPasswordDto>
    {
        private readonly SendEmailCommand _sendEmailCommand;
        private readonly EndpointConfig _endpointConfig;

        public ResetPasswordCommand(ILogger logger, BaseDbContext dbContext, SendEmailCommand sendEmailCommand, EndpointConfig endpointConfig) : base(logger, dbContext)
        {
            _sendEmailCommand = sendEmailCommand;
            _endpointConfig = endpointConfig;
        }

        public override async Task<OperationResult> Execute(ResetPasswordDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Login))
                return new OperationSucceded();

            User user = await _dbContext.Users.SingleAsync(u => u.EmailVerified && u.CandidateId == request.Login);
            var passwordReset = new PasswordReset()
            {
                UserId = user.Id,
                Id = Guid.NewGuid(),
                InsertDateTime = DateTime.UtcNow
            };
            _dbContext.PasswordResets.Add(passwordReset);
            await _dbContext.SaveChangesAsync();

            _sendEmailCommand.Execute(new EmailDto()
            {
                To=user.Email,
                Title="Reset Password",
                Body = _endpointConfig.SetNewPassword(passwordReset.Id.ToString())
            });

            return new OperationSucceded();
        }
    }
}
