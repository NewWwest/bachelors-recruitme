using System;
using System.Collections.Generic;
using System.Text;
using db = RecruitMe.Logic.Data.Entities;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class ExamTakerDto
    {
        public int? Id { get; set; }

        public int ExamId { get; set; }

        public string ExamCategoryName { get; set; }

        public int UserId { get; set; }

        public string CandidateId { get; set; }

        public DateTime StartDate { get; set; }

        public float? Score { get; set; }

        public string UserDisplayName { get; set; }

        public int? TeacherId { get; set; }

        public string TeacherDisplayName { get; set; }


        public db.ExamTaker ToEntity()
        {
            if (Id.HasValue)
            {
                return new db.ExamTaker()
                {
                    Id = Id.Value,
                    ExamId = ExamId,
                    UserId = UserId,
                    StartDate = StartDate,
                    Score = Score,
                    TeacherId = TeacherId
                };
            }
            else
            {
                return new db.ExamTaker()
                {
                    ExamId = ExamId,
                    UserId = UserId,
                    StartDate = StartDate,
                    Score = Score,
                    TeacherId = TeacherId
                };
            }
        }

        public static ExamTakerDto FromEntities(db.ExamTaker entity, db.User user, db.Teacher teacher, string examCategoryName)
        {
            return new ExamTakerDto()
            {
                Id = entity.Id,
                ExamId = entity.ExamId,
                UserId = user.Id,
                CandidateId = user.CandidateId,
                UserDisplayName = string.Concat(user.Name, " ", user.Surname),
                StartDate = entity.StartDate,
                Score = entity.Score,
                TeacherId = entity.TeacherId,
                TeacherDisplayName = teacher == null ? "" : string.Concat(teacher.Name, " ", teacher.Surname),
                ExamCategoryName = examCategoryName
            };
        }
    }
}
