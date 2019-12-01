using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Pesel { get; set; }

        public string CandidateId { get; set; }

        public string PasswordHash { get; set; }

        public bool EmailVerified { get; set; }
    }
}
