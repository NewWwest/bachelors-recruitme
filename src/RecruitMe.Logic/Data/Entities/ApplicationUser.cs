using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Pesel { get; set; }

        public string CandidateId { get; set; }
    }
}
