using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class AddOrUpdateProfileDataCommandRequest
    {
        public int UserId { get; set; }

        public ProfileDataDto Data { get; set; }
    }
}
