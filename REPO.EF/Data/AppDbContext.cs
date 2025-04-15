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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed Data
            var Difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = 1,
                    Name="Easy"
                },

                 new Difficulty()
                {
                    Id = 2,
                    Name="Medium"
                },
                  new Difficulty()
                {
                    Id = 3,
                    Name="Hard"
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(Difficulties);


            var regions = new List<Region>()
            {
                new Region() {
                    Id=Guid.Parse("CE898FE2-16AD-403B-9D5F-DD0D98CAD958"),
                    Name="Auckland",
                    Code="AKL",
                    RegionImageURL="image1",
                    

                },
                 new Region() {
                    Id=Guid.Parse("CE898FE2-26AD-403B-9D5F-DD0D98CAD958"),
                    Name="Nelson",
                    Code="NSN",
                    RegionImageURL="image2"
                },
                   new Region() {
                    Id=Guid.Parse("CE898FE2-36AD-403B-9D5F-DD0D98CAD958"),
                    Name="Southland",
                    Code="STL",
                    RegionImageURL="image3"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions); 
        }

    }
}
