using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RecruitMe.Logic.Operations.Administration.Exam
{
    public class LoadExaminationSheetRequest
    {
        public int ExamId { get; set; }

        public Stream FileStream { get; set; }

        public int TeacherId { get; set; }
    }
}
