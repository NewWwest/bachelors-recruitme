using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class ExamTaker
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string CandidateId { get; set; }

        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }

        public DateTime StartDate { get; set; }

        public float? Score { get; set; }

        [ForeignKey(nameof(Teacher))]
        public int? TeacherId { get; set; } //Egzaminator

        public Teacher Teacher { get; set; }

        public Exam Exam { get; set; }

        public User User { get; set; }
    }
}
