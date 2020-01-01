using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RecruitMe.Logic.Data.Entities
{
    public class Message
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(From))]
        public int FromId { get; set; }

        [ForeignKey(nameof(To))]
        public int ToId { get; set; }

        public string Text { get; set; }

        public bool IsRead { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual User From { get; set; }

        public virtual User To { get; set; }
    }
}
