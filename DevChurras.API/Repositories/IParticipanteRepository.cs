using DevChurras.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevChurras.API.Repositories
{
    public interface IParticipanteRepository
    {
        Task<List<Participante>> GetAllAsync();

        Task<Participante> GetByIdAsync(int id);

        Task AddAsync(Participante participante);

        Task DeleteAsync(Participante participante);

        Task<bool> ParticipanteExistsAsync(int id);
    }
}