using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RecrutMe.Logic.Data.Entities
{
    public class ExampleClass
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
