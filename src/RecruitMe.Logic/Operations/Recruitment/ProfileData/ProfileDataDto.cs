using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileData
{
    public class ProfileDataDto
    {
        public string Adress { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }

        public string PrimarySchool { get; set; }

        public static ProfileDataDto FromPersonalDataEntity(PersonalData entity)
        {
            var result = new ProfileDataDto()
            {
                Adress = entity?.Adress,
                FatherName = entity?.FatherName,
                MotherName = entity?.MotherName,
                PrimarySchool = entity?.PrimarySchool
            };
            return result;
        }
    }
}
