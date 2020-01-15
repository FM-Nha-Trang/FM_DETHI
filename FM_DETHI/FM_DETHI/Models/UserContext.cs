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

        public DbSet<FM_DETHI.Models.Tests> Tests { get; set; }
    }
}
