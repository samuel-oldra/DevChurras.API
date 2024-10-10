using DevChurras.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DevChurras.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Convidado> Convidados { get; set; }

        public DbSet<Participante> Participantes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}