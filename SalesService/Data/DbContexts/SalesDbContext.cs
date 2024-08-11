using Microsoft.EntityFrameworkCore;
using SalesService.Enitities;

namespace SalesService.Data.DbContexts
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
            
        }

        public DbSet<Sale> Sales { get; set; }  
    }
}
