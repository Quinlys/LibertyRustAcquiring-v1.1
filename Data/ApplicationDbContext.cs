using Microsoft.EntityFrameworkCore;

namespace LibertyRustAcquiring.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Models.Entities.Order> Orders { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<PackItem> PackItems { get; set; }
    }
}
