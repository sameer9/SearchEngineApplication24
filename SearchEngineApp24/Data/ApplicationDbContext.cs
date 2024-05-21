using Microsoft.EntityFrameworkCore;
using SearchEngineApp24.Models;

namespace SearchEngineApp24.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PizzaStore> PizzaStores { get; set; }
    }

}
