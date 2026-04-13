using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AzureWebApp.Models;

namespace AzureWebApp.Data
{
    public class AzureWebAppContext : DbContext
    {
        public AzureWebAppContext (DbContextOptions<AzureWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AzureWebApp.Models.Students> Students { get; set; } = default!;
    }
}
