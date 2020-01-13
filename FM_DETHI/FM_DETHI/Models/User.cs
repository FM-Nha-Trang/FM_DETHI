using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DETHI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public Int16 Age { get; set; }
        public string Gender { get; set; }

        public bool Check_username(string Username)
        {
            if (Username == this.Username)
                return true;
            return false;
        } 
        public int create(User users)
        {

            return 0;
        }
    }
}
