using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class BusinessConfiguration
    {
        public static DateTime LowestRegistrationDate => new DateTime(2000, 1, 1);
        public static DateTime HighestRegistrationDate => new DateTime(2005, 1, 1);
    }
}
