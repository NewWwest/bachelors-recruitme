using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class LoadExaminationSheetCommand : BaseAsyncOperation<OperationResult, (int id, Stream fileStream)>
    {
        public LoadExaminationSheetCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public override Task<OperationResult> Execute((int id, Stream fileStream) request)
        {
            throw new NotImplementedException();
        }
    }
}
