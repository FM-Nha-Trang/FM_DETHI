using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FM_DETHI.Models;
using Microsoft.EntityFrameworkCore;

namespace DETHI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Tests> Tests { get; set; }

        public DbSet<History_Answer> History_Answer { get; set; }

        public DbSet<Questions> Questions { get; set; }
    }
}
