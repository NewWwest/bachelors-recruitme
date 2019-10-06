using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class PersonalData
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
