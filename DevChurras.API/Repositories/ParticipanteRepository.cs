using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevChurras.API.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly DataContext context;

        public ParticipanteRepository(DataContext context) =>
            this.context = context;

        public async Task<List<Participante>> GetAllAsync() =>
            await context.Participantes.ToListAsync();

        public async Task<Participante> GetByIdAsync(int id) =>
            await context.Participantes.SingleOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Participante participante)
        {
            context.Participantes.Add(participante);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Participante participante)
        {
            context.Participantes.Remove(participante);

            await context.SaveChangesAsync();
        }

        public async Task<bool> ParticipanteExistsAsync(int id) =>
            await context.Participantes.AnyAsync(p => p.Id == id);
    }
}