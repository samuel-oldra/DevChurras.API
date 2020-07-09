using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minhaApiWeb.Data;
using minhaApiWeb.Models;

namespace minhaApiWeb.Controllers
{
    [ApiController]
    [Route("participantes")]
    public class ParticipanteController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Participante>>> Get([FromServices] DataContext context)
        {
            var participantes = await context.Participantes.ToListAsync();
            return participantes;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Participante>> Post(
            [FromServices] DataContext context,
            [FromBody] Participante model)
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
    }
}