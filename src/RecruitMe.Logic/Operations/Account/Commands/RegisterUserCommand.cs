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
    public class RegisterUserCommand : BaseAsyncOperation<int, RegisterDto, RegisterRequestValidator>
    {
        private readonly PasswordHasher _passwordHasher;

        public RegisterUserCommand(ILogger logger,
            RegisterRequestValidator validator,
            BaseDbContext dbContext,
            PasswordHasher passwordHasher) : base(logger, validator, dbContext)
        {
            _passwordHasher = passwordHasher;
        }

        protected async override Task<int> DoExecute(RegisterDto request)
        {
            var candidateprefix = request.Name.Substring(0, request.Name.Length > 3 ? 3 : request.Name.Length) +
                request.Surname.Substring(0, request.Surname.Length > 3 ? 3 : request.Surname.Length);
            var candidadateIds = _dbContext.Users
                .Where(u => u.CandidateId.StartsWith(candidateprefix))
                .Select(u=>u.CandidateId).ToList();

            string candidateId = null;
            for (int i = 0; i < 1000; i++)
            {
                candidateId = candidateprefix + i.ToString("000");
                if (!candidadateIds.Contains(candidateId))
                    break;
            }

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                Pesel = request.Pesel,
                CandidateId = candidateId,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                EmailVerified = false
            };

            var result = _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            if (result.IsKeySet)
            {
                return result.Entity.Id;
            }

            throw new Exception("Registration Failed");
        }
    }
}
