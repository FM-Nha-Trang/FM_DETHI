using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FM_DETHI.Models
{
    public class Questions
    {
        [Key]
        public int id { get; set; }
        public string Test_code { get; set; }
        public string Title { get; set; }
        public string Answer_A { get; set; }
        public string Answer_B { get; set; }
        public string Answer_C { get; set; }
        public string Answer_D { get; set; }
        public string Answer_true { get; set; }
    }
}
