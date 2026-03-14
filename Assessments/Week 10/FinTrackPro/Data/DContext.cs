using Microsoft.EntityFrameworkCore;
using FinTrackPro.Models;

namespace FinTrackPro.Data
{
    public class DContext : DbContext
    {
        public DContext(DbContextOptions<DContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}