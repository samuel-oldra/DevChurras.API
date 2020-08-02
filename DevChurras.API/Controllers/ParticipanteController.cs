using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/participante")]
    public class ParticipanteController : ControllerBase
    {
        // GET: api/participante
        /// <summary>
        /// Listagem de Participantes
        /// </summary>
        /// <param name="context">DataContext</param>
        /// <returns>Lista de Participantes</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {
            var participantes = await context.Participantes.ToListAsync();

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
        /// <param name="context">DataContext</param>
        /// <param name="model">Dados do Participante</param>
        /// <returns>Objeto criado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromServices] DataContext context, [FromBody] Participante model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Participantes.Add(model);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        // DELETE: api/participante/{id}
        /// <summary>
        /// Deleta um Participante
        /// </summary>
        /// <param name="context">DataContext</param>
        /// <param name="id">ID do Participante</param>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Participante não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] DataContext context, int id)
        {
            // Remove participante
            var participante = await context.Participantes.SingleOrDefaultAsync(p => p.Id == id);

            if (participante == null)
                return NotFound("Participante não encontrado");

            context.Participantes.Remove(participante);

            // Remove convidado do participante
            var convidado = await context.Convidados.SingleOrDefaultAsync(c => c.ParticipanteId == id);

            if (convidado != null)
                context.Convidados.Remove(convidado);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}