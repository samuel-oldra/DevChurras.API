using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevChurras.API.Repositories
{
    public class ConvidadoRepository : IConvidadoRepository
    {
        private readonly DataContext context;

        public ConvidadoRepository(DataContext context) =>
            this.context = context;

        public async Task<List<Convidado>> GetAllAsync() =>
            await context.Convidados.Include(c => c.Participante).ToListAsync();

        public async Task<Convidado> GetByIdAsync(int id) =>
            await context.Convidados.SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Convidado> GetByParticipanteIdAsync(int participanteId) =>
            await context.Convidados.SingleOrDefaultAsync(c => c.ParticipanteId == participanteId);

        public async Task AddAsync(Convidado convidado)
        {
            context.Convidados.Add(convidado);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Convidado convidado)
        {
            context.Convidados.Remove(convidado);

            await context.SaveChangesAsync();
        }

        public async Task<bool> ConvidadoByParticipanteIdExistsAsync(int participanteId) =>
            await context.Convidados.AnyAsync(c => c.ParticipanteId == participanteId);
    }
}