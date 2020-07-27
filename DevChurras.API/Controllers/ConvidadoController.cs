using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/convidado")]
    public class ConvidadoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {
            var convidados = await context.Convidados.Include(c => c.Participante).ToListAsync();

            return Ok(convidados);
        }

        [HttpPost]
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

        [HttpDelete("{id}")]
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