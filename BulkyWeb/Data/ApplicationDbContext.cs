using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Data
{

    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 2 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 1 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }

                );
        }








        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configurationRoot = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("").Build();
        //        optionsBuilder.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection"));
        //    }




        //}

    }
}
