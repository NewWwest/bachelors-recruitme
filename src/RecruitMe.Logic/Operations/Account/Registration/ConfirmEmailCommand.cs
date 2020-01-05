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

namespace RecruitMe.Logic.Operations.Account.Registration
{
    public class ConfirmEmailCommand : BaseAsyncOperation<string, Guid>
    {
        private readonly SendEmailCommand _sendEmailCommand;

        public ConfirmEmailCommand(ILogger logger, BaseDbContext dbContext, SendEmailCommand sendEmailCommand) : base(logger, dbContext)
        {
            _sendEmailCommand = sendEmailCommand;
        }

        public override async Task<string> Execute(Guid request)
        {
            var email = _dbContext.ConfirmationEmails
                .Include(ce=>ce.User)
                .FirstOrDefault(ce => ce.Id == request);

            if (email == null)
                throw new EmailVerificationTokenNotFoundException();

            if (email.Used)
            {
                return email.User.CandidateId;
            }

            email.Used = true;

            var user = email.User;
            user.EmailVerified = true;
            await AssignCandidateId(user);
            await _dbContext.SaveChangesAsync();

            _sendEmailCommand.Execute(new EmailDto()
            {
                To = user.Email,
                Title = EmailContentConfiguration.EmailVerifiedTitle,
                Body = EmailContentConfiguration.EmailVerifiedBody(user.CandidateId)
            });

            return user.CandidateId;
        }

        private async Task AssignCandidateId(User user)
        {
            string name = user.Name.ToLowerInvariant();
            string surname = user.Surname.ToLowerInvariant();
            string candidateprefix =
                name.Substring(0, name.Length > 3 ? 3 : name.Length) +
                surname.Substring(0, surname.Length > 3 ? 3 : surname.Length);

            List<string> candidadateIds = await _dbContext.Users
                .Where(u => u.CandidateId.StartsWith(candidateprefix))
                .Select(u => u.CandidateId)
                .ToListAsync();

            string candidateId = null;
            for (int i = 0; i < 1000; i++)
            {
                string check = candidateprefix + i.ToString("000");
                if (!candidadateIds.Contains(check))
                {
                    candidateId = check;
                    break;
                }
            }
            if (candidateId == null)
                _logger.Log("Critical error - out of UserIds");

            user.CandidateId = candidateId;
        }
    }
}
