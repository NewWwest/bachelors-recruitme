using Microsoft.AspNetCore.Http;
using RecruitMe.Logic.Data;
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
            var fileId = await _fileStorage.SaveAsync(request.File, request.FileName);

            return "";
        }
    }
}
