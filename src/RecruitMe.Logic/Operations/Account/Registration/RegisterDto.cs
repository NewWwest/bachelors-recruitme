using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitMe.Logic.Operations.Account.Registration
{
    public class RegisterDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Pesel { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
