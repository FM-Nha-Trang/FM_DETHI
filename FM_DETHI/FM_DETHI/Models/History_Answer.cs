using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FM_DETHI.Models
{
    public class History_Answer
    {
        public History_Answer()
        {
        }

        public History_Answer(int userid, string test_code, DateTime date_answer, int point)
        {
            this.userid = userid;
            this.test_code = test_code;
            this.date_answer = date_answer;
            this.point = point;
        }

        [Key]
        public int id { get; set; }
        public int userid { get; set; }
        public string test_code { get; set; }
        [Column(TypeName ="datetime")]
        public DateTime date_answer { get; set; }
        public int point { get; set; }

    }
}
