using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Teacher
{
    public class GetTeachersQuery : BaseAsyncOperation<IEnumerable<TeacherDto>>
    {
        public GetTeachersQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<IEnumerable<TeacherDto>> Execute()
        {
            return (await _dbContext.Teachers.ToListAsync()).Select(ec => TeacherDto.FromEntity(ec));
        }
    }
}
