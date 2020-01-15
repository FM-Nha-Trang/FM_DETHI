using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FM_DETHI.Models
{
    public class Tests
    {
        [Key]
        public string test_code { get; set; }
        public int user_create { get; set; }
        public string date_create { get; set; }
        public string title { get; set; }
    }
}
