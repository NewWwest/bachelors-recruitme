using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Messages.Dto
{
    public class UserThreadDto
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public int NewMessagesCount { get; set; }
    }
}
