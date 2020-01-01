using RecruitMe.Logic.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Messages.Dto
{
    public class GetMessagesDto
    {
        public int From { get; set; }
        public int To { get; set; }
        public PagingParameters Parameters { get; set; }
    }
}
