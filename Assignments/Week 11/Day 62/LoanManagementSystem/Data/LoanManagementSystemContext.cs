using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Data
{
    public class LoanManagementSystemContext : DbContext
    {
        public LoanManagementSystemContext (DbContextOptions<LoanManagementSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Loan> Laptop { get; set; } = default!;
    }
}
