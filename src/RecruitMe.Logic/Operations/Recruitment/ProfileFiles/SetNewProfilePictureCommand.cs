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
    public class SetNewProfilePictureCommand : BaseAsyncOperation<string, FileRequest>
    {
        private readonly IFileRepository _fileStorage;
        private readonly IPictureSaver _pictureSaver;

        public SetNewProfilePictureCommand(ILogger logger, BaseDbContext dbContext, IFileRepository fileStorage, IPictureSaver pictureSaver) : base(logger, dbContext)
        {
            _fileStorage = fileStorage;
            _pictureSaver = pictureSaver;
        }

        public override async Task<string> Execute(FileRequest request)
        {
            var profile = await _dbContext.PersonalData
                .Include(p => p.ProfilePictureFile)
                .FirstOrDefaultAsync(p => p.UserId == request.UserId);

            if (profile != null && profile.ProfilePictureFileId > 0)
            {
                var file = profile.ProfilePictureFile;
                _fileStorage.Delete(file.FileUrl);
                _dbContext.PersonalDocuments.Remove(file);
            }

            var fileId = await _pictureSaver.SaveAsync(request.File, request.FileName);
            var newFileRecord = new PersonalDocument()
            {
                FileUrl = fileId,
                Name = request.FileName,
                UserId = request.UserId,
                ContentType = request.ContentType
            };

            await _dbContext.PersonalDocuments.AddAsync(newFileRecord);
            if (profile != null)
            {
                profile.ProfilePictureFileId = newFileRecord.Id;
            }
            else
            {
                profile = new PersonalData()
                {
                    UserId = request.UserId,
                    ProfilePictureFileId = newFileRecord.Id,
                };
                await _dbContext.PersonalData.AddAsync(profile);
            }
            await _dbContext.SaveChangesAsync();
            return fileId;
        }
    }
}
