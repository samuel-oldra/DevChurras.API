using DevChurras.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DevChurras.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Participante> Participantes { get; set; }

        public DbSet<Convidado> Convidados { get; set; }
    }
}