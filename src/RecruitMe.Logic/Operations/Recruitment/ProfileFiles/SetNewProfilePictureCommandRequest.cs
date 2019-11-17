using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RecruitMe.Logic.Operations.Recruitment.ProfileFiles
{
    public class SetNewProfilePictureCommandRequest
    {
        public int UserId { get; set; }

        public string FileName { get; set; }

        public Stream File { get; set; }
    }
}
