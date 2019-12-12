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
            var user = await _dbContext.Users
                .Include(u => u.PersonalData)
                .Include(u=>u.PersonalDocuments)
                .FirstOrDefaultAsync(u => u.Id == request);
            var data = user.PersonalData;
            var docs = user.PersonalDocuments;

            var result = ProfileDataDto.FromPersonalDataEntity(user, data, docs);

            return result;
        }
    }
}
