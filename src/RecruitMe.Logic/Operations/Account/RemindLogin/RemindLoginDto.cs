using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.RemindLogin
{
    public class RemindLoginDto
    {
        public string Email { get; set; }

        public string Pesel { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
