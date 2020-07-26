using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("")]
    public class ConvidadoController : ControllerBase
    {
        [HttpGet]
        [Route("listar_convidados")]
        public async Task<ActionResult<List<Convidado>>> Get([FromServices] DataContext context)
        {
            var convidados = await context.Convidados
                .Include(c => c.Participante)
                .ToListAsync();

            return Ok(convidados);
        }

        [HttpPost]
        [Route("adicionar_convidado")]
        public async Task<ActionResult<Convidado>> Post(
            [FromServices] DataContext context,
            [FromBody] Convidado model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Participante não encontrado
            var hasParticipante = await context.Participantes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ParticipanteId == model.ParticipanteId);

            if (hasParticipante == null)
                return NotFound("Participante não encontrado");

            // Participante já possui um convidado
            var hasConvidado = await context.Convidados
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ParticipanteId == model.ParticipanteId);

            if (hasConvidado != null)
                return BadRequest("Participante já possui um convidado");

            context.Convidados.Add(model);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet]
        [Route("remover_convidado/{id:int}")]
        public async Task<ActionResult<Convidado>> Get(
            [FromServices] DataContext context,
            int id)
        {
            var convidado = await context.Convidados
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ConvidadoId == id);

            if (convidado == null)
                return NotFound("Convidado não encontrado");

            context.Convidados.Remove(convidado);
            await context.SaveChangesAsync();

            return Ok(convidado);
        }
    }
}