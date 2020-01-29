using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Messages.Dto
{
    public class SendDto
    {
        public int FromId { get; set; }
        public string ToId { get; set; }
        public string Message { get; set; }
    }
}
