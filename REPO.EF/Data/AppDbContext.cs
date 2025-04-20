using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using REPO.Core.Models;

namespace REPO.EF.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }


      // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      // {
      //     if (!optionsBuilder.IsConfigured)
      //     {
      //         optionsBuilder.UseSqlServer( );
      //       //  optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NZWalksdb;Integrated Security=True;Trust Server Certificate=True");
      //     }
      // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>()
             .HasIndex(u => u.Email)
             .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();





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


            var roleReaderId = Guid.NewGuid();
            var roleWriterId = Guid.NewGuid();
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=roleReaderId.ToString(),
                    ConcurrencyStamp=roleReaderId.ToString(),
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=roleWriterId.ToString(),
                    ConcurrencyStamp=roleWriterId.ToString(),  // protect data integrity in edge cases (like concurrent updates via an admin panel),
                    Name="Writer",
                    NormalizedName="Writer".ToUpper() //Identity automatically uses NormalizedName when checking for roles or usernames.
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);



        }

    }
}
