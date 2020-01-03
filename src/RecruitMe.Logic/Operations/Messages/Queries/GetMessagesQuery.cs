using Microsoft.EntityFrameworkCore;
using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
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
        public GetMessagesQuery(ILogger logger, GetMessagesValidator validator, BaseDbContext dbContext) : base(logger, validator, dbContext)
        {
        }

        protected async override Task<PagedResponse<MessageDto>> DoExecute(GetMessagesDto request)
        {
            var messagesTask = _dbContext.Messages.Where(m =>
                                    m.FromId == request.From && m.ToId == request.To ||
                                    m.ToId == request.From && m.FromId == request.To)
                               .OrderByDescending(m => m.Timestamp)
                               .Skip((request.Parameters.Page - 1) * request.Parameters.PageSize)
                               .Take(request.Parameters.PageSize)
                               .ToListAsync();
            var countTask = _dbContext.Messages.CountAsync();

            await Task.WhenAll(messagesTask, countTask);

            List<Message> messages = messagesTask.Result;
            int count = countTask.Result;

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
