using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Logic.Operations.Administration.Candidate
{
    public class GetCandidatesResultDto
    {
        public string CandidateId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
