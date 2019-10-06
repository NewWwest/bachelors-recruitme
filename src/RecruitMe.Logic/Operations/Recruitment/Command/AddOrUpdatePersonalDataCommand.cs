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
        private readonly BaseDbContext _db;

        public AddOrUpdatePersonalDataCommand(ILogger logger, AddOrUpdatePersonalDataCommandRequestValidator validator, BaseDbContext db) : base(logger, validator)
        {
            _db = db;
        }

        protected override async Task<bool> DoExecute(AddOrUpdatePersonalDataCommandRequest request)
        {
            var entity = new PersonalData() {
                UserId=request.UserId,
                Name = request.Data.Name,
                Surname = request.Data.Surname
            };

            try
            {
                _db.PersonalData.Add(entity);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception exc)
            {
                _logger.Log(exc);
                _db.PersonalData.Update(entity);
                await _db.SaveChangesAsync();
                return true;
            }
        }
    }
}
