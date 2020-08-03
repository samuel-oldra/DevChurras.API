using DevChurras.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevChurras.API.Repositories
{
    public interface IConvidadoRepository
    {
        Task<List<Convidado>> GetAllAsync();

        Task<Convidado> GetByIdAsync(int id);

        Task<Convidado> GetByParticipanteIdAsync(int participanteId);

        Task AddAsync(Convidado convidado);

        Task DeleteAsync(Convidado convidado);

        Task<bool> ConvidadoByParticipanteIdExistsAsync(int participanteId);
    }
}