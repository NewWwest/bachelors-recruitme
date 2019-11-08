using Microsoft.AspNetCore.Http;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileFiles
{
    public class SetNewProfilePictureCommand : BaseAsyncOperation<string, SetNewProfilePictureCommandRequest>
    {
        public SetNewProfilePictureCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        protected override async Task<string> DoExecute(SetNewProfilePictureCommandRequest request)
        {
            return null;
        }
    }
}
