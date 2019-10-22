using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.Dto
{
    public class PersonalDataDto
    {
        public string Adress { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }

        public string PrimarySchool { get; set; }

        public static PersonalDataDto FromPersonalDataEntity(PersonalData entity)
        {
            var result = new PersonalDataDto()
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
