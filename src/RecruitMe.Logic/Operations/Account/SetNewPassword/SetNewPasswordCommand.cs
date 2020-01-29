using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Account.Helpers;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Account.SetNewPassword
{
    public class SetNewPasswordCommand : BaseAsyncOperation<OperationResult, SetNewPasswordDto, SetNewPasswordValidator>
    {
        private readonly PasswordHasher _passwordHasher;

        public SetNewPasswordCommand(ILogger logger,
            SetNewPasswordValidator validator, 
            BaseDbContext dbContext,
            PasswordHasher passwordHasher) : base(logger, validator, dbContext)
        {
            _passwordHasher = passwordHasher;
        }

        protected override async Task<OperationResult> DoExecute(SetNewPasswordDto request)
        {
            var reset = await _dbContext.PasswordResets
                .Include(pr=>pr.User)
                .SingleAsync(pr => pr.Id == request.Token);

            var user = reset.User;

            user.PasswordHash = _passwordHasher.HashPassword(request.Password);
            _dbContext.PasswordResets.Remove(reset);

            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
