using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Dto
{
    public class LoggedInUserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
