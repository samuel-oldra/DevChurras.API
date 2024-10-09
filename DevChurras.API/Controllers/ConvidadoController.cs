using DevChurras.API.Models;
using DevChurras.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/convidado")]
    public class ConvidadoController : ControllerBase
    {
        private readonly IConvidadoRepository convidadoRepository;

        private readonly IParticipanteRepository participanteRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="convidadoRepository"></param>
        /// <param name="participanteRepository"></param>
        public ConvidadoController(
            IConvidadoRepository convidadoRepository,
            IParticipanteRepository participanteRepository)
        {
            this.convidadoRepository = convidadoRepository;
            this.participanteRepository = participanteRepository;
        }

        // GET: api/convidado
        /// <summary>
        /// Listagem de Convidados
        /// </summary>
        /// <returns>Lista de Convidados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var convidados = await convidadoRepository.GetAllAsync();

            return Ok(convidados);
        }

        // POST: api/convidado
        /// <summary>
        /// Cadastro de Convidado
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "nome": "Arthur",
        ///     "consomeBebidaAlcoolica": false,
        ///     "participanteId": 1
        /// }
        /// </remarks>
        /// <param name="model">Dados do Convidado</param>
        /// <returns>Objeto criado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Participante não encontrado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(Convidado model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Participante não encontrado
            var hasParticipante = await participanteRepository.ParticipanteExistsAsync(model.ParticipanteId);

            if (!hasParticipante)
                return NotFound("Participante não encontrado");

            // Participante já possui um convidado
            var hasConvidado = await convidadoRepository.ConvidadoByParticipanteIdExistsAsync(model.ParticipanteId);

            if (hasConvidado)
                return BadRequest("Participante já possui um convidado");

            await convidadoRepository.AddAsync(model);

            return Ok(model);
        }

        // DELETE: api/convidado/{id}
        /// <summary>
        /// Deleta um Convidado
        /// </summary>
        /// <param name="id">ID do Convidado</param>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Convidado não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var convidado = await convidadoRepository.GetByIdAsync(id);

            if (convidado == null)
                return NotFound("Convidado não encontrado");

            await convidadoRepository.DeleteAsync(convidado);

            return NoContent();
        }
    }
}