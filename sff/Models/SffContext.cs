using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sff.Models
{
    public class SffContext : DbContext
    {
        public SffContext(DbContextOptions<SffContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SffDatabas.db");
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<MovieStudio> MovieStudios { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PostEtikett>PostEtiketts { get; set; }


        //Sätter constraints på databasen när modellen skapas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(m => m.Id).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.Title).IsRequired();
            modelBuilder.Entity<Movie>().Property(m => m.RentLimit).HasDefaultValue(1);

            modelBuilder.Entity<Studio>().Property(s => s.Id).IsRequired();
            modelBuilder.Entity<Studio>().Property(s => s.City).IsRequired();
            modelBuilder.Entity<Studio>().Property(s => s.Name).IsRequired();

            modelBuilder.Entity<MovieStudio>().HasIndex(ms => new { ms.MovieId, ms.StudioId }).IsUnique();

            modelBuilder.Entity<Review>().Property(r => r.Id).IsRequired();
            modelBuilder.Entity<Review>().Property(r => r.Rating).HasDefaultValue(0);
            modelBuilder.Entity<Review>().Property(r => r.MovieId).IsRequired();
            modelBuilder.Entity<Review>().Property(r => r.StudioId).IsRequired();

        }
    }
}
