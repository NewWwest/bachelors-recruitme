using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class ExamCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ExamType ExamType { get; set; }

        public virtual IEnumerable<Exam> Exams { get; set; }
    }
}
