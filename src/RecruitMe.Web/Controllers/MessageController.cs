using Microsoft.AspNetCore.Mvc;
using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Messages.Commands;
using RecruitMe.Logic.Operations.Messages.Dto;
using RecruitMe.Logic.Operations.Messages.Queries;
using RecruitMe.Logic.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitMe.Web.Controllers
{
    [Route("api/messages/")]
    public class MessageController : RecruitMeBaseController
    {
        [HttpGet]
        [Route("checknewmessages")]
        public async Task<ActionResult> CheckNewMessages()
        {
            User user = await AuthenticateUser();
            int count = await Get<GetNewMessagesCountQuery>().Execute(user);

            return Json(count);
        }

        [HttpGet]
        [Route("{withId}/")]
        public async Task<ActionResult> GetMessages(string withId, [FromQuery] PagingParameters parameters)
        {   
            User user = await AuthenticateUser();
            int toId = await Get<GetAdminOrUserIdQuery>().Execute(withId);

            GetMessagesDto request = new GetMessagesDto()
            {
                From = user.Id,
                To = toId,
                Parameters = parameters
            };

            PagedResponse<MessageDto> messages = await Get<GetMessagesQuery>().Execute(request);
            return Json(messages);
        }

        [HttpGet]
        [Route("getUserThreads")]
        public async Task<ActionResult> GetUserThreads()
        {
            User admin = await AuthenticateAdmin();
            IEnumerable<UserThreadDto> threadDtos = await Get<GetUserThreadsQuery>().Execute(admin);

            return Json(threadDtos);
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> SendMessage([FromBody] SendDto sendDto)
        {
            if (sendDto.FromId == 0)
            {
                User user = await AuthenticateUser();
                sendDto.FromId = user.Id;
            }

            MessageDto message = await Get<SendNewMessageCommand>().Execute(sendDto);

            return Json(message);
        }
    }
}
