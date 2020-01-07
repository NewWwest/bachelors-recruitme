using RecruitMe.Logic.Data;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Logging;
using RecruitMe.Logic.Operations.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitMe.Logic.Operations.Messages.Commands
{
    public class SetQueriedMessagesAsReadCommand : BaseAsyncOperation<OperationResult, IEnumerable<Message>>
    {
        public SetQueriedMessagesAsReadCommand(ILogger logger, BaseDbContext dbContext) : base(logger, dbContext)
        {
        }

        public async override Task<OperationResult> Execute(IEnumerable<Message> messages)
        {
            if (!messages.Any()) return new OperationSucceded();

            _dbContext.Messages.AttachRange(messages);
            foreach (Message message in messages)
            {
                message.IsRead = true;
            }

            _dbContext.Messages.UpdateRange(messages);
            await _dbContext.SaveChangesAsync();

            return new OperationSucceded();
        }
    }
}
