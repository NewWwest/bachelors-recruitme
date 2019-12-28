using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.MyRecruitment
{
    public class ExamsAndStatusDto
    {
        public RecrutationStatus Status { get; set; }

        public List<ExamDataDto> Exams { get; set; }
    }
}
