using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class ConfirmationEmail
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }

        public bool Used { get; set; }

    }
}
