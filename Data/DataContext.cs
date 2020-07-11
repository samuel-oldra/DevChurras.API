using minhaApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace minhaApiWeb.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) {

        }

        /*
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Participante> ()
                .HasOne<Convidado> (p => p.Convidado)
                .WithOne (c => c.Participante)
                .HasForeignKey<Convidado> (c => c.ParticipanteRef);
        }
        */

        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Convidado> Convidados { get; set; }
    }
}