using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class ExamDto
    {
        public int? Id { get; set; }

        public int SeatCount { get; set; }

        public DateTime StartDateTime { get; set; }

        public int DurationInMinutes { get; set; }

        public int ExamCategoryId { get; set; }


        public Data.Entities.Exam ToEntity()
        {
            if (Id.HasValue)
            {
                return new Data.Entities.Exam()
                {
                    Id = Id.Value,
                    SeatCount = SeatCount,
                    StartDateTime = StartDateTime,
                    DurationInMinutes = DurationInMinutes,
                    ExamCategoryId = ExamCategoryId
                };
            }
            else
            {
                return new Data.Entities.Exam()
                {
                    SeatCount = SeatCount,
                    StartDateTime = StartDateTime,
                    DurationInMinutes = DurationInMinutes,
                    ExamCategoryId = ExamCategoryId
                };
            }
        }

        public static ExamDto FromEntity(Data.Entities.Exam entity)
        {
            return new ExamDto()
            {
                Id = entity.Id,
                SeatCount = entity.SeatCount,
                StartDateTime = entity.StartDateTime,
                DurationInMinutes = entity.DurationInMinutes,
                ExamCategoryId = entity.ExamCategoryId
            };

        }
    }
}
