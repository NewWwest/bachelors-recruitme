using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class AddOrUpdateProfileDataCommand : BaseAsyncOperation<OperationResult, AddOrUpdateProfileDataCommandRequest, AddOrUpdateProfileDataCommandRequestValidator>
    {
        public AddOrUpdateProfileDataCommand(ILogger logger, AddOrUpdateProfileDataCommandRequestValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(AddOrUpdateProfileDataCommandRequest request)
        {
            var data = await _dbContext.PersonalData.FirstOrDefaultAsync(e => e.UserId == request.UserId);
            if (data == null)
            {
                data = new PersonalData()
                {
                    Adress = request.Data.Adress,
                    FatherName = request.Data.FatherName,
                    MotherName = request.Data.MotherName,
                    PrimarySchool = request.Data.PrimarySchool,
                    UserId = request.UserId
                };
                _dbContext.PersonalData.Add(data);
            }
            else
            {
                data.Adress = request.Data.Adress;
                data.FatherName = request.Data.FatherName;
                data.MotherName = request.Data.MotherName;
                data.PrimarySchool = request.Data.PrimarySchool;
            }

            await _dbContext.SaveChangesAsync();
            return new OperationSucceded();
        }
    }
}
