using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Messages.Dto
{
    public class MessageDto
    {
        public bool IsMine { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
