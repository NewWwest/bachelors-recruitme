using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public string DotpayOperationNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
