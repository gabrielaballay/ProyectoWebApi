using Microsoft.EntityFrameworkCore;

namespace PetFinder.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
        public DbSet<Encontrada> Encontradas { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
    }
}
