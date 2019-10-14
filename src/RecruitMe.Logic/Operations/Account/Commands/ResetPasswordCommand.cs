using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
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
    public class ResetPasswordCommand : BaseAsyncOperation<bool, ResetPasswordDto, ResetPasswordValidator>
    {
        private readonly PasswordHasher _passwordHasher;

        public ResetPasswordCommand(ILogger logger, 
            ResetPasswordValidator validator, 
            BaseDbContext dbContext,
            PasswordHasher passwordHasher) : base(logger, validator, dbContext)
        {
            _passwordHasher = passwordHasher;
        }

        protected override async Task<bool> DoExecute(ResetPasswordDto request)
        {
            var reset = await _dbContext.PasswordResets
                .Include(pr=>pr.User)
                .SingleAsync(pr => pr.Id == request.PasswordResetId);

            var user = reset.User;

            user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
