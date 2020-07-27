using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/participante")]
    public class ParticipanteController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] DataContext context)
        {
            var participantes = await context.Participantes.ToListAsync();

            return Ok(participantes);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] Participante model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Participantes.Add(model);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] DataContext context, int id)
        {
            // Remove participante
            var participante = await context.Participantes.SingleOrDefaultAsync(p => p.Id == id);
            if (participante == null) return NotFound("Participante não encontrado");

            context.Participantes.Remove(participante);

            // Remove convidado do participante
            var convidado = await context.Convidados.SingleOrDefaultAsync(c => c.ParticipanteId == id);
            if (convidado != null) context.Convidados.Remove(convidado);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}