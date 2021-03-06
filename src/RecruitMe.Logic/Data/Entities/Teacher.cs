using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public virtual IEnumerable<ExamTaker> ExamTakers { get; set; }
    }
}
