using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using RecruitMe.Logic.Operations.Messages.Commands;
using RecruitMe.Logic.Operations.Messages.Dto;
using RecruitMe.Logic.Operations.Messages.Validators;
using RecruitMe.Logic.Utilities.Paging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Messages.Queries
{
    public class GetMessagesQuery : BaseAsyncOperation<PagedResponse<MessageDto>, GetMessagesDto, GetMessagesValidator>
    {
        private readonly SetQueriedMessagesAsReadCommand _setQueriedMessagesAsReadCommand;

        public GetMessagesQuery(ILogger logger, GetMessagesValidator validator, BaseDbContext dbContext,
            SetQueriedMessagesAsReadCommand setQueriedMessagesAsReadCommand) : base(logger, validator, dbContext)
        {
            _setQueriedMessagesAsReadCommand = setQueriedMessagesAsReadCommand;
        }

        protected async override Task<PagedResponse<MessageDto>> DoExecute(GetMessagesDto request)
        {
            if (request.Parameters.Page < 1)
                request.Parameters.Page = 1;
            
            var queriedMessages = _dbContext.Messages.Where(m =>
                                    m.FromId == request.From && m.ToId == request.To ||
                                    m.ToId == request.From && m.FromId == request.To);

            var messagesTask = queriedMessages.OrderByDescending(m => m.Timestamp)
                                   .Skip((request.Parameters.Page - 1) * request.Parameters.PageSize)
                                   .Take(request.Parameters.PageSize)
                                   .ToListAsync();
            var countTask = queriedMessages.CountAsync();

            await Task.WhenAll(messagesTask, countTask);

            List<Message> messages = messagesTask.Result;
            int count = countTask.Result;

            // Set received messages as read
            await _setQueriedMessagesAsReadCommand.Execute(messages.Where(m => !m.IsRead));

            PagedResponse<MessageDto> response = new PagedResponse<MessageDto>()
            {
                Page = request.Parameters.Page,
                TotalCount = count,
                Data = messages.Select(m => new MessageDto()
                {
                    IsMine = m.FromId == request.From,
                    Message = m.Text,
                    Timestamp = m.Timestamp
                })
            };

            return response;
        }
    }
}
