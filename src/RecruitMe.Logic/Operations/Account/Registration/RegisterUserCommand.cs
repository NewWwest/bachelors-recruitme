using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Email;
using RecruitMe.Logic.Configuration;
using System;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Registration
{
    public class RegisterUserCommand : BaseAsyncOperation<int, RegisterDto, RegisterRequestValidator>
    {
        private readonly PasswordHasher _passwordHasher;
        private readonly SendEmailCommand _sendEmailCommand;

        public RegisterUserCommand(ILogger logger,
            RegisterRequestValidator validator,
            BaseDbContext dbContext,
            PasswordHasher passwordHasher,
            SendEmailCommand sendEmailCommand) : base(logger, validator, dbContext)
        {
            _passwordHasher = passwordHasher;
            _sendEmailCommand = sendEmailCommand;
        }

        protected async override Task<int> DoExecute(RegisterDto request)
        {
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Pesel = request.Pesel,
                CandidateId = null,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                EmailVerified = false,
                BirthDate = request.BirthDate.Value
            };

            EntityEntry<User> result = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();


            if (!result.IsKeySet)
            {
                throw new Exception("Registration Failed");
            }

            Guid token = await GenerateEmailConfirmationToken(result.Entity.Id);
            _sendEmailCommand.Execute(new EmailDto()
            {
                Body = url(token),
                Title = "Complete Recruit Me registration",
                To = user.Email
            });
            return result.Entity.Id;
        }

        private async Task<Guid> GenerateEmailConfirmationToken(int userId)
        {
            var token = Guid.NewGuid();
            _dbContext.ConfirmationEmails.Add(new ConfirmationEmail()
            {
                Id = token,
                UserId = userId,
                Used = false
            });
            await _dbContext.SaveChangesAsync();
            return token;
        }

        private string url(Guid token)
        {
            return EndpointConfig.BaseAddress + EndpointConfig.ConfirmEmail + "/" + token.ToString();
        }
    }
}
