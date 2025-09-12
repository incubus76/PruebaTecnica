using System.Data.Entity;

namespace PruebaTecnica.Models
{
    public class MiDbContext : DbContext
    {
        public MiDbContext() : base("DefaultConnection") { }

        public DbSet<Producto> Productos { get; set; }
    }
}