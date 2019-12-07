using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Administration.Teacher
{
    public class TeacherDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public Data.Entities.Teacher ToEntity()
        {
            if (Id.HasValue)
            {
                return new Data.Entities.Teacher()
                {
                    Id = Id.Value,
                    Name = Name,
                    Surname = Surname,
                    Email=Email
                };
            }
            else
            {
                return new Data.Entities.Teacher()
                {
                    Name = Name,
                    Surname = Surname,
                    Email = Email
                };
            }
        }

        public static TeacherDto  FromEntity(Data.Entities.Teacher entity)
        {
            return new TeacherDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Email = entity.Email
            };
        }
    }
}
