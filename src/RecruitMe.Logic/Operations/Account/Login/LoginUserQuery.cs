using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account.Helpers;
using System;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.Login
{
    public class LoginUserQuery : BaseAsyncOperation<User, LoginDto, LoginRequestValidator>
    {
        private readonly PasswordHasher _passwordHasher;

        public LoginUserQuery(ILogger logger, 
            LoginRequestValidator validator,
            BaseDbContext dbContext,
            PasswordHasher passwordHasher) : base(logger, validator, dbContext)
        {
            _passwordHasher = passwordHasher;
        }


        protected override async Task<User> DoExecute(LoginDto request)
        {
            User user = await _dbContext.Users.SingleAsync(u => u.CandidateId == request.CandidateId);
            var result = _passwordHasher.VerifyPassword(user, request.Password);

            if (result)
            {
                return user;
            }

            throw new UnauthorizedAccessException();
        }
    }
}
