using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.ExamCategory
{
    public class GetExamCategoriesQuery : BaseAsyncOperation<IEnumerable<ExamCategoryDto>>
    {
        public GetExamCategoriesQuery(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override async Task<IEnumerable<ExamCategoryDto>> Execute()
        {
            return (await _dbContext.ExamCategories.ToListAsync()).Select(ec => ExamCategoryDto.FromEntity(ec));
        }
    }
}
