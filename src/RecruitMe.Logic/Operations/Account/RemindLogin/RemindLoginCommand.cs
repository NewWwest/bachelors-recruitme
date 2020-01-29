using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.RemindLogin
{
    public class RemindLoginCommand : BaseAsyncOperation<OperationResult, RemindLoginDto, RemindLoginValidator>
    {
        private readonly SendEmailCommand _sendEmailCommand;

        public RemindLoginCommand(ILogger logger, RemindLoginValidator validator, BaseDbContext dbContext, SendEmailCommand sendEmailCommand) : base(logger, validator, dbContext)
        {
            _sendEmailCommand = sendEmailCommand;
        }

        protected override async Task<OperationResult> DoExecute(RemindLoginDto request)
        {
            User user;
            if (string.IsNullOrEmpty(request.Pesel))
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email &&
                u.EmailVerified == true &&
                u.Pesel == null &&
                u.Name == request.Name &&
                u.Surname == request.Surname);
            }
            else
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email &&
                u.EmailVerified == true &&
                u.Pesel == request.Pesel);
            }

            if(user == null)
            {
                //Security issue: always return 200 even if user was no found
                return new OperationSucceded(); 
            }

            _sendEmailCommand.Execute(new EmailDto()
            {
                To = user.Email,
                Title = EmailContentConfiguration.LoginRemindedTitle,
                Body = EmailContentConfiguration.LoginRemindedBody(user.CandidateId)
            });
            return new OperationSucceded();
        }
    }
}
