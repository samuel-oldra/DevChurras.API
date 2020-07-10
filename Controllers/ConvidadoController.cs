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
            var convidados = await context.Convidados.Include (x => x.Participante).ToListAsync ();
            return convidados;
        }

        [HttpPost]
        [Route ("")]
        public async Task<ActionResult<Convidado>> Post ([FromServices] DataContext context, [FromBody] Convidado model) {
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