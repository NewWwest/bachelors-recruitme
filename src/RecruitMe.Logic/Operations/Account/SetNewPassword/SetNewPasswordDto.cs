using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.SetNewPassword
{
    public class SetNewPasswordDto
    {
        public Guid Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
