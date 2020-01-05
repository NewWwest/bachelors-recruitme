using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class BusinessConfiguration
    {
        public int AllowedAccountsWithSameEmail { get; set; }
        public string AdminLogin { get; set; }
        public string InitialAdminPassword { get; set; }
        public string InitialAdminPasswordHash { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public DateTime LowestRegistrationDate { get; set; }
        public DateTime HighestRegistrationDate { get; set; }
        public string DefaultProfileImagePath { get; set; }
        public string BaseAddress { get; set; }
        public string BaseAddressNoSsl { get; set; }
    }
}
