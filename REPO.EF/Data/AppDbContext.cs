using Microsoft.EntityFrameworkCore;
using REPO.Core.Models;

namespace REPO.EF.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Walk> Walks { get; set; }

       

    }
}
