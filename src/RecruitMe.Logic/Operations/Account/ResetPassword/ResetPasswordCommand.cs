using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Common;
using RecruitMe.Logic.Operations.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.ResetPassword
{
    public class ResetPasswordCommand : BaseAsyncOperation<OperationResult, ResetPasswordDto>
    {
        private readonly SendEmailCommand _sendEmailCommand;

        public ResetPasswordCommand(ILogger logger, BaseDbContext dbContext, SendEmailCommand sendEmailCommand) : base(logger, dbContext)
        {
            _sendEmailCommand = sendEmailCommand;
        }

        protected override async Task<OperationResult> DoExecute(ResetPasswordDto request)
        {
            var user = await _dbContext.Users.SingleAsync(u => u.EmailVerified && u.CandidateId == request.Login);
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
                Body = "http://localhost:5000/resetpassword?token={token}"
            });

            return new OperationSucceded();
        }
    }
}
