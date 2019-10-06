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
        public BaseDbContext _db;

        public GetPersonalDataQuery(ILogger logger, BaseDbContext db) : base(logger)
        {
            _db = db;
        }

        protected override async Task<PersonalDataDto> DoExecute(int request)
        {
            var entity = await _db.PersonalData.FirstOrDefaultAsync(pd => pd.UserId == request);

            var result = new PersonalDataDto()
            {
                Name = entity?.Name,
                Surname = entity?.Surname
            };
            return result;
        }
    }
}
