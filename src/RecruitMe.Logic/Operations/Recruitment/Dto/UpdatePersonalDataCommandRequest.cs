using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.Dto
{
    public class AddOrUpdatePersonalDataCommandRequest
    {
        public int UserId { get; set; }

        public PersonalDataDto Data { get; set; }
    }
}
