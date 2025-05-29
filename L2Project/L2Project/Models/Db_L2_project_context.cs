using L2Project.Models.Pedidos;
using Microsoft.EntityFrameworkCore;

namespace L2Project.Models
{
    public class Db_L2_project_context : DbContext
    {
        public Db_L2_project_context(DbContextOptions<Db_L2_project_context> dbContext) : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Models.Pedidos.Pedidos> Pedidos { get; set; }
    }
}
