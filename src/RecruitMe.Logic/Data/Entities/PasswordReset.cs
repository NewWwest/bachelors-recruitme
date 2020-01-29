using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class PasswordReset
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public DateTime InsertDateTime { get; set; }

        public User User { get; set; }
    }
}
