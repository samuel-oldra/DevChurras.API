using DevChurras.API.Models;
using DevChurras.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/participante")]
    public class ParticipanteController : ControllerBase
    {
        private readonly IConvidadoRepository convidadoRepository;

        private readonly IParticipanteRepository participanteRepository;

        public ParticipanteController(
            IConvidadoRepository convidadoRepository,
            IParticipanteRepository participanteRepository)
        {
            this.convidadoRepository = convidadoRepository;
            this.participanteRepository = participanteRepository;
        }

        // GET: api/participante
        /// <summary>
        /// Listagem de Participantes
        /// </summary>
        /// <returns>Lista de Participantes</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var participantes = await participanteRepository.GetAllAsync();

            return Ok(participantes);
        }

        // POST: api/participante
        /// <summary>
        /// Cadastro de Participante
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "nome": "Samuel",
        ///     "consomeBebidaAlcoolica": true
        /// }
        /// </remarks>
        /// <param name="model">Dados do Participante</param>
        /// <returns>Objeto criado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Participante model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await participanteRepository.AddAsync(model);

            return Ok(model);
        }

        // DELETE: api/participante/{id}
        /// <summary>
        /// Deleta um Participante
        /// </summary>
        /// <param name="id">ID do Participante</param>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Participante não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            // Remove participante
            var participante = await participanteRepository.GetByIdAsync(id);

            if (participante == null)
                return NotFound("Participante não encontrado");

            await participanteRepository.DeleteAsync(participante);

            // Remove convidado do participante
            var convidado = await convidadoRepository.GetByParticipanteIdAsync(id);

            if (convidado != null)
                await convidadoRepository.DeleteAsync(convidado);

            return NoContent();
        }
    }
}