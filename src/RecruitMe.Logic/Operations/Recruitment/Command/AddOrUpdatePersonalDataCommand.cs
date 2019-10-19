using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using RecruitMe.Logic.Operations.Recruitment.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.Command
{
    public class AddOrUpdatePersonalDataCommand : BaseAsyncOperation<OperationResult, AddOrUpdatePersonalDataCommandRequest, AddOrUpdatePersonalDataCommandRequestValidator>
    {
        public AddOrUpdatePersonalDataCommand(ILogger logger, AddOrUpdatePersonalDataCommandRequestValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<OperationResult> DoExecute(AddOrUpdatePersonalDataCommandRequest request)
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
