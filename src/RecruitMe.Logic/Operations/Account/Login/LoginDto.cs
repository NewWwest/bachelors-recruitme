using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Login
{
    public class LoginDto
    {
        public string CandidateId { get; set; }

        public string Password { get; set; }
    }
}
