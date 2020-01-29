using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class PersonalDocument
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string FileUrl { get; set; }

        public string ContentType { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
