using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        public int SeatCount { get; set; }

        public DateTime StartDateTime { get; set; }

        public int DurationInMinutes { get; set; }

        [ForeignKey(nameof(ExamCategory))]
        public int ExamCategoryId { get; set; }
        
        public virtual ExamCategory ExamCategory { get; set; }

        public virtual IEnumerable<ExamTaker> ExamTakers { get; set; }
    }
}
