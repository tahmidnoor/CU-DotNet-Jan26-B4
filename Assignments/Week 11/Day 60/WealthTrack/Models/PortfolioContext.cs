using Microsoft.EntityFrameworkCore;

namespace WealthTrack.Models
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }

        public DbSet<Investment> Investment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investment>()
                .Property(p => p.PurchasePrice)
                .HasPrecision(18, 2); // ✅ FIX
        }
    }
}
