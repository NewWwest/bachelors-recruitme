using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileFiles
{
    public class SaveFileCommand : BaseAsyncOperation<string, FileRequest>
    {
        private readonly IFileSaver _fileStorage;

        public SaveFileCommand(ILogger logger, BaseDbContext dbContext, IFileSaver fileStorage) : base(logger, dbContext)
        {
            _fileStorage = fileStorage;
        }

        protected override async Task<string> DoExecute(FileRequest request)
        {
            var fileId = await _fileStorage.SaveAsync(request.File, request.FileName);
            var newFileRecord = new PersonalDocument()
            {
                FileUrl = fileId,
                Name = request.FileName,
                UserId = request.UserId,
                ContentType = request.ContentType
            };

            await _dbContext.PersonalDocuments.AddAsync(newFileRecord);

            await _dbContext.SaveChangesAsync();

            return fileId;
        }
    }
}
