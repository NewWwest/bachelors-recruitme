using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileFiles
{
    public class DeleteFileCommand : BaseAsyncOperation<OperationResult, (int UserId, int FileId)>
    {
        private readonly IFileRepository _filestorage;

        public DeleteFileCommand(ILogger logger, BaseDbContext dbContext, IFileRepository filestorage) : base(logger, dbContext)
        {
            _filestorage = filestorage;
        }

        public override async Task<OperationResult> Execute((int UserId, int FileId) request)
        {
            var document = _dbContext.PersonalDocuments.FirstOrDefault(pd => pd.UserId == request.UserId && pd.Id == request.FileId);
            if (document == null)
            {
                return new OperationFailed();
            }
            _dbContext.PersonalDocuments.Remove(document);
            _filestorage.Delete(document.FileUrl);
            await _dbContext.SaveChangesAsync();

            return new OperationSucceded();
        }
    }
}
