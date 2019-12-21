using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class BusinessConfiguration
    {
        public string AdminLogin { get; set; }
        public string InitialAdminPassword { get; set; }
        public string InitialAdminPasswordHash { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public DateTime LowestRegistrationDate { get; set; }
        public DateTime HighestRegistrationDate { get; set; }
        /// <summary>
        /// Remember to check applicationUrl in launchsettings
        /// </summary>
        public string BaseAddress { get; set; }
    }
}
