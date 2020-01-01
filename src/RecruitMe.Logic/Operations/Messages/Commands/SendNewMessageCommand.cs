using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Configuration;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Dto;
using RecruitMe.Logic.Operations.Messages.Queries;
using RecruitMe.Logic.Operations.Messages.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Messages.Commands
{
    public class SendNewMessageCommand : BaseAsyncOperation<OperationResult, SendDto, SendDtoValidator>
    {
        private readonly GetAdminOrUserIdQuery _getAdminOrUserIdQuery;

        public SendNewMessageCommand(ILogger logger, SendDtoValidator validator, BaseDbContext dbContext,
            GetAdminOrUserIdQuery getAdminOrUserIdQuery) : base(logger, validator, dbContext)
        {
            _getAdminOrUserIdQuery = getAdminOrUserIdQuery;
        }

        protected async override Task<OperationResult> DoExecute(SendDto request)
        {
            int toId = await _getAdminOrUserIdQuery.Execute(request.ToId);

            Message message = new Message()
            {
                FromId = request.FromId,
                IsRead = false,
                Text = request.Message,
                Timestamp = DateTime.Now,
                ToId = toId,
            };

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return new OperationSucceded();
        }
    }
}
