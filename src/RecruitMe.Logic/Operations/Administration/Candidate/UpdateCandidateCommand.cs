using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.ProfileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class UpdateCandidateCommand : BaseAsyncOperation<OperationResult, ProfileDataDto>
    {
        private readonly AddOrUpdateProfileDataCommand _addOrUpdateProfileDataCommand;

        public UpdateCandidateCommand(ILogger logger, BaseDbContext dbContext, AddOrUpdateProfileDataCommand addOrUpdateProfileDataCommand) : base(logger, dbContext)
        {
            _addOrUpdateProfileDataCommand = addOrUpdateProfileDataCommand;
        }

        public override async Task<OperationResult> Execute(ProfileDataDto request)
        {
            Data.Entities.User user = await _dbContext.Users.FirstAsync(u => u.Id == request.UserId);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            user.Email = request.Email;
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Pesel = request.Pesel;
            user.CandidateId = request.CandidateId;
            user.BirthDate = request.BirthDate;
            await _dbContext.SaveChangesAsync();
                ;
            await _addOrUpdateProfileDataCommand.Execute(new AddOrUpdateProfileDataCommandRequest()
            {
                UserId = request.UserId,
                Data = new ProfileDataDto()
                {
                    Adress = request.Adress,
                    FatherName = request.FatherName,
                    MotherName = request.MotherName,
                    PrimarySchool = request.PrimarySchool
                }
            });

            return new OperationSucceded();
        }
    }
}
