using System.Collections.Generic;
using System.Threading.Tasks;
using minhaApiWeb.Data;
using minhaApiWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace minhaApiWeb.Controllers {
    [ApiController]
    [Route ("convidados")]
    public class ConvidadoController : ControllerBase {
        [HttpGet]
        [Route ("")]
        public async Task<ActionResult<List<Convidado>>> Get ([FromServices] DataContext context) {
            var convidados = await context.Convidados
                .Include (p => p.Participante)
                .ToListAsync ();
            return convidados;
        }

        [HttpPost]
        [Route ("")]
        public async Task<ActionResult<Convidado>> Post ([FromServices] DataContext context, [FromBody] Convidado model) {
            // Participante não encontrado
            var hasParticipante = await context.Participantes
                .AsNoTracking ()
                .FirstOrDefaultAsync (x => x.ParticipanteId == model.ParticipanteId);
            if (hasParticipante == null)
                return BadRequest ("Participante não encontrado");

            // Participante já possui um convidado
            var hasConvidado = await context.Convidados
                .AsNoTracking ()
                .FirstOrDefaultAsync (x => x.ParticipanteId == model.ParticipanteId);
            if (hasConvidado != null)
                return BadRequest ("Participante já possui um convidado");

            if (ModelState.IsValid) {
                context.Convidados.Add (model);
                await context.SaveChangesAsync ();
                return model;
            } else {
                return BadRequest (ModelState);
            }
        }
    }
}