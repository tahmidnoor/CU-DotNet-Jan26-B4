using Microsoft.EntityFrameworkCore;
using Vagabond.Service.Models;

namespace Vagabond.Service.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Destination> Destinations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>(entity =>
            {
                entity.Property(d => d.CityName)
                      .IsRequired();

                entity.Property(d => d.Country)
                      .IsRequired();

                entity.Property(d => d.Description)
                      .HasMaxLength(200);

                entity.Property(d => d.Rating)
                      .HasDefaultValue(3);
            });
        }
    }
}