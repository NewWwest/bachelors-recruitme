using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.Queries
{
    public class GetPersonalDataQuery : BaseAsyncOperation<PersonalDataDto, int>
    {
        public GetPersonalDataQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override async Task<PersonalDataDto> DoExecute(int request)
        {
            var entity = await _dbContext.PersonalData.FirstOrDefaultAsync(pd => pd.UserId == request);

            var result = new PersonalDataDto()
            {

            };
            return result;
        }
    }
}
