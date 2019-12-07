using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Configuration
{
    public class BusinessConfiguration
    {
        public static string AdminLogin => "admin";
        public static string InitialAdminPassword => "ZMIEN_TO_HASLO";
        public static string InitialAdminPasswordHash => @"qUCJK+8XBQX1VctuCSy/47SGSXP/cVtDzNfoLhir+wWBpPZe";





        public static string Email => "RecruitMeSystem@gmail.com";
        public static string EmailPassword => "Tester123!";

        public static DateTime LowestRegistrationDate => new DateTime(2000, 1, 1);
        public static DateTime HighestRegistrationDate => new DateTime(2005, 1, 1);
    }
}
