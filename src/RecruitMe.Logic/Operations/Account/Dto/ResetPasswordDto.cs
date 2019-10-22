using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Dto
{
    public class ResetPasswordDto
    {
        public Guid PasswordResetId { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}
