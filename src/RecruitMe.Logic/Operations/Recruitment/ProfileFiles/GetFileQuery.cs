using Microsoft.EntityFrameworkCore;
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
    public class GetFileQueryRequest 
    {
        public int UserId { get; set; }
        public int FileId { get; set; }
    }

    public class GetFileQuery : BaseAsyncOperation<Stream, GetFileQueryRequest>
    {
        private readonly IFileStorage _fileStorage;

        public GetFileQuery(ILogger logger, BaseDbContext dbContext, IFileStorage fileStorage) : base(logger, dbContext)
        {
            _fileStorage = fileStorage;
        }

        protected override async Task<Stream> DoExecute(GetFileQueryRequest request)
        {
            var doc = await _dbContext.PersonalDocuments
                .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Id == request.FileId);

            return _fileStorage.Get(doc.FileUrl);
        }
    }
}
