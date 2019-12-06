using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Administration.ExamCategory
{
    public class ExamCategoryDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public ExamType ExamType { get; set; }

        public Data.Entities.ExamCategory ToEntity()
        {
            if (Id.HasValue)
            {
                return new Data.Entities.ExamCategory()
                {
                    Id = Id.Value,
                    ExamType = ExamType,
                    Name = Name
                };
            }
            else
            {
                return new Data.Entities.ExamCategory()
                {
                    ExamType = ExamType,
                    Name = Name
                };
            }
        }

        public static ExamCategoryDto  FromEntity(Data.Entities.ExamCategory entity)
        {
            return new ExamCategoryDto()
            {
                Id = entity.Id,
                ExamType = entity.ExamType,
                Name = entity.Name
            };
            
        }
    }
}
