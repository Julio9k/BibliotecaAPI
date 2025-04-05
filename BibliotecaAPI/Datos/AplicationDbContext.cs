using BibliotecaAPI.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Datos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Autor> Autores{ get; set; }
        protected AplicationDbContext()
        {
        }
    }
}
