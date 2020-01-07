using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;
using RecruitMe.Logic.Operations.Messages.Queries;
using RecruitMe.Logic.Operations.Messages.Validators;
using RecruitMe.Logic.Utilities.Dates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Messages.Commands
{
    public class SendNewMessageCommand : BaseAsyncOperation<MessageDto, SendDto, SendDtoValidator>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly GetAdminOrUserIdQuery _getAdminOrUserIdQuery;

        public SendNewMessageCommand(ILogger logger, SendDtoValidator validator, BaseDbContext dbContext,
            GetAdminOrUserIdQuery getAdminOrUserIdQuery, IDateTimeProvider dateTimeProvider) : base(logger, validator, dbContext)
        {
            _dateTimeProvider = dateTimeProvider;
            _getAdminOrUserIdQuery = getAdminOrUserIdQuery;
        }

        protected async override Task<MessageDto> DoExecute(SendDto request)
        {
            int toId = await _getAdminOrUserIdQuery.Execute(request.ToId);

            Message message = new Message()
            {
                FromId = request.FromId,
                IsRead = false,
                Text = request.Message,
                Timestamp = _dateTimeProvider.Now,
                ToId = toId,
            };

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return new MessageDto()
            {
                IsMine = true,
                Message = message.Text,
                Timestamp = message.Timestamp
            };
        }
    }
}
