using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/convidado")]
    public class ConvidadoController : ControllerBase
    {
        // GET: api/convidado
        /// <summary>
        /// Listagem de Convidados
        /// </summary>
        /// <returns>Lista de Convidados</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {
            var convidados = await context.Convidados.Include(c => c.Participante).ToListAsync();

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
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] Convidado model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Participante não encontrado
            var hasParticipante = await context.Participantes.AnyAsync(p => p.Id == model.ParticipanteId);
            if (!hasParticipante) return NotFound("Participante não encontrado");

            // Participante já possui um convidado
            var hasConvidado = await context.Convidados.AnyAsync(c => c.ParticipanteId == model.ParticipanteId);
            if (hasConvidado) return BadRequest("Participante já possui um convidado");

            context.Convidados.Add(model);
            await context.SaveChangesAsync();

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
        public async Task<IActionResult> Delete([FromServices] DataContext context, int id)
        {
            var convidado = await context.Convidados.SingleOrDefaultAsync(c => c.Id == id);
            if (convidado == null) return NotFound("Convidado não encontrado");

            context.Convidados.Remove(convidado);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}