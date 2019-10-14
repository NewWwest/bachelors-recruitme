using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Commands
{
    public class SendPasswordResetLinkCommand : BaseAsyncOperation<bool, string, EmailValidator>
    {
        public SendPasswordResetLinkCommand(ILogger logger, EmailValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<bool> DoExecute(string request)
        {
            var user = await _dbContext.Users.SingleAsync(u => u.EmailVerified && u.CandidateId == request);
            var passwordReset = new PasswordReset()
            {
                UserId = user.Id,
                Id = Guid.NewGuid(),
                InsertDateTime = DateTime.UtcNow
            };
            _dbContext.PasswordResets.Add(passwordReset);
            await _dbContext.SaveChangesAsync();

            //SEND EMAIL

            return true;
        }
    }
}
