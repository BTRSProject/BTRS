using BTRS.Models;
using Microsoft.EntityFrameworkCore;

namespace BTRS.DATA
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Admin> admin { set; get; }
        public DbSet<Bus> bus { set; get; }

        public DbSet<Pass_Trips> pass_trips { set; get; }
        public DbSet<Passengers> passengers { set; get; }
        public DbSet<Trips> trips { set; get; }
    }
}
