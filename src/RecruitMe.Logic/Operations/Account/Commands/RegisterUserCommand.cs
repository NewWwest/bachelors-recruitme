using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Account.Dto;
using RecruitMe.Logic.Operations.Account.Helpers;
using RecruitMe.Logic.Operations.Account.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Commands
{
    public class RegisterUserCommand : BaseAsyncOperation<User, RegisterDto, RegisterRequestValidator>
    {
        private readonly PasswordHasher _passwordHasher;

        public RegisterUserCommand(ILogger logger,
            RegisterRequestValidator validator,
            BaseDbContext dbContext,
            PasswordHasher passwordHasher) : base(logger, validator, dbContext)
        {
            _passwordHasher = passwordHasher;
        }

        protected async override Task<User> DoExecute(RegisterDto request)
        {
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Pesel = request.Pesel,
                CandidateId = null,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                EmailVerified = false
            };

            var result = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            if (result.IsKeySet)
            {
                return result.Entity;
            }

            throw new Exception("bad registration");
        }
    }
}
