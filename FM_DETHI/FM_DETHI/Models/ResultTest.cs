using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FM_DETHI.Models
{
    public class ResultTest
    {
        public string test_code { get; set; }
        public int user_create { get; set; }
        public string date_create { get; set; }
        public string title { get; set; }
        public bool isAnswer { get; set; }
        public History_Answer dataAnswer { get; set; }

        public ResultTest(string test_code, int user_create, string date_crate, string title, bool isAnswer, History_Answer dataAnswer)
        {
            this.test_code = test_code;
            this.user_create = user_create;
            this.date_create = date_create;
            this.title = title;
            this.isAnswer = isAnswer;
            this.dataAnswer = dataAnswer;
        }
    }
}
