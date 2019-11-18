using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class GetProfileDataQuery : BaseAsyncOperation<ProfileDataDto, int>
    {
        public GetProfileDataQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override async Task<ProfileDataDto> DoExecute(int request)
        {
            var data = await _dbContext.PersonalData.FirstOrDefaultAsync(pd => pd.UserId == request);
            var docs = await _dbContext.PersonalDocuments.Where(doc => doc.UserId == request).ToListAsync();

            var result = ProfileDataDto.FromPersonalDataEntity(data, docs);

            return result;
        }
    }
}
