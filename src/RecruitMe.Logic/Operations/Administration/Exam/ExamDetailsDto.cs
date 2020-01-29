using RecruitMe.Logic.Data.Entities;
using RecruitMe.Logic.Operations.Administration.ExamCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class ExamDetailsDto
    {
        public ExamDto Exam { get; set; }

        public List<ExamTakerDto> ExamTakers { get; set; }
    }
}
