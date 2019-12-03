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
    public class SetNewProfilePictureCommand : BaseAsyncOperation<string, SetNewProfilePictureCommandRequest>
    {
        private readonly IFileStorage _fileStorage;

        public SetNewProfilePictureCommand(ILogger logger, BaseDbContext dbContext, IFileStorage fileStorage) : base(logger, dbContext)
        {
            _fileStorage = fileStorage;
        }

        protected override async Task<string> DoExecute(SetNewProfilePictureCommandRequest request)
        {
            var profile = await _dbContext.PersonalData
                .Include(p => p.ProfilePictureFile)
                .FirstOrDefaultAsync(p => p.UserId == request.UserId);

            if (profile != null && profile.ProfilePictureFileId > 0)
            {
                var file = profile.ProfilePictureFile;
                _fileStorage.Delete(file.FileUrl);
            }

            var fileId = await _fileStorage.SaveAsync(request.File, request.FileName);
            var newFileRecord = new PersonalDocument()
            {
                FileUrl = fileId,
                Name = request.FileName,
                UserId = request.UserId,
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
