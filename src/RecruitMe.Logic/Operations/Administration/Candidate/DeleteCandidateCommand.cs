using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class DeleteCandidateCommand : BaseAsyncOperation<OperationResult, int>
    {
        public DeleteCandidateCommand(ILogger logger, BaseDbContext dbContext) : base(logger,  dbContext)
        {
        }

        public override async Task<OperationResult> Execute(int request)
        {
            _dbContext.PersonalDocuments.RemoveRange(
                _dbContext.PersonalDocuments.Where(pd=>pd.UserId==request)
                );
            _dbContext.ConfirmationEmails.RemoveRange(
                _dbContext.ConfirmationEmails.Where(pd => pd.UserId == request)
                );
            _dbContext.ExamTakers.RemoveRange(
                _dbContext.ExamTakers.Where(pd => pd.UserId == request)
                );
            _dbContext.PasswordResets.RemoveRange(
                _dbContext.PasswordResets.Where(pd => pd.UserId == request)
                );
            _dbContext.PersonalData.RemoveRange(
                _dbContext.PersonalData.Where(pd => pd.UserId == request)
                );
            _dbContext.Users.Remove(
                _dbContext.Users.Find(request)
                );
            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
