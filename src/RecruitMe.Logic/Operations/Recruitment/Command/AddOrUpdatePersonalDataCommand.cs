using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Recruitment.Dto;
using RecruitMe.Logic.Operations.Recruitment.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Recruitment.Command
{
    public class AddOrUpdatePersonalDataCommand : BaseAsyncOperation<bool, AddOrUpdatePersonalDataCommandRequest, AddOrUpdatePersonalDataCommandRequestValidator>
    {
        public AddOrUpdatePersonalDataCommand(ILogger logger, AddOrUpdatePersonalDataCommandRequestValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected override async Task<bool> DoExecute(AddOrUpdatePersonalDataCommandRequest request)
        {
            var entity = new PersonalData()
            {
                UserId = request.UserId,
                Name = request.Data.Name,
                Surname = request.Data.Surname
            };

            try
            {
                _dbContext.PersonalData.Add(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception exc)
            {
                _logger.Log(exc);
                _dbContext.PersonalData.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
