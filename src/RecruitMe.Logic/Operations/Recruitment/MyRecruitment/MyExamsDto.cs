using RecruitMe.Logic.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.MyRecruitment
{
    public class MyExamsDto
    {
        public List<MyExamDto> Exams { get; set; }
    }

    public class MyExamDto
    {
        public int DurationInMinutes { get; set; }

        public DateTime StartTime { get; set; }

        public string CategoryName { get; set; }

        public ExamType ExamType { get; set; }
    }
}
