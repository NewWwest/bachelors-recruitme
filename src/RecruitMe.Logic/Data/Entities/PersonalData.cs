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
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public string Adress { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }

        public string PrimarySchool { get; set; }

        public int? ProfilePictureFileId { get; set; }

        public virtual PersonalDocument ProfilePictureFile { get; set; }

        public virtual IEnumerable<PersonalDocument> Documents { get; set; }

        public virtual User User { get; set; }
    }
}
