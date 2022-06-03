using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minhaApiWeb.Data;
using minhaApiWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace minhaApiWeb.Controllers
{
    [ApiController]
    [Route("")]
    public class ParticipanteController : ControllerBase
    {
        [HttpGet]
        [Route("listar_participantes")]
        public async Task<ActionResult<List<Participante>>> Get([FromServices] DataContext context)
        {
            var participantes = await context.Participantes.ToListAsync();
            return participantes;
        }

        [HttpPost]
        [Route("adicionar_participante")]
        public async Task<ActionResult<Participante>> Post([FromServices] DataContext context, [FromBody] Participante model)
        {
            if (ModelState.IsValid)
            {
                context.Participantes.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("remover_participante/{id:int}")]
        public async Task<ActionResult<Participante>> Get([FromServices] DataContext context, int id)
        {
            // Remove participante
            var participante = await context.Participantes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ParticipanteId == id);
            if (participante == null)
                return BadRequest("Participante não encontrado");
            context.Participantes.Remove(participante);

            // Remove convidado do participante
            var convidado = await context.Convidados
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ParticipanteId == id);
            if (convidado != null)
                context.Convidados.Remove(convidado);

            await context.SaveChangesAsync();
            return participante;
        }
    }
}