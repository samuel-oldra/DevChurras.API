using Microsoft.EntityFrameworkCore;
using minhaApiWeb.Models;

using minhaApiWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Participante> Participantes { get; set; }
    }
}