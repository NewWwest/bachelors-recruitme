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
    public class GetFileQueryResult :IDisposable
    {
        public Stream Data { get; set; }
        public string ContentType { get; set; }

        public void Dispose()
        {
            Data.Dispose();
        }
    }

    public class GetFileQuery : BaseAsyncOperation<GetFileQueryResult, (int UserId, int FileId)>
    {
        private readonly IFileRepository _fileStorage;

        public GetFileQuery(ILogger logger, BaseDbContext dbContext, IFileRepository fileStorage) : base(logger, dbContext)
        {
            _fileStorage = fileStorage;
        }

        public override async Task<GetFileQueryResult> Execute((int UserId, int FileId) request)
        {
            var doc = await _dbContext.PersonalDocuments
                .FirstOrDefaultAsync(d => d.UserId == request.UserId && d.Id == request.FileId);

            return new GetFileQueryResult()
            {
                Data = _fileStorage.Get(doc.FileUrl),
                ContentType = doc.ContentType,
            };
        }
    }
}
