using System.Collections.Generic;
using System.Threading.Tasks;
using minhaApiWeb.Data;
using minhaApiWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace minhaApiWeb.Controllers {
    [ApiController]
    [Route ("")]
    public class TotalController : ControllerBase {
        [HttpPost]
        [Route ("totais")]
        public async Task<ActionResult<Total>> Post ([FromServices] DataContext context, [FromBody] Total model) {
            int valorPorPessoa = 20;
            int valorPorPessoaQueNaoBebe = 10;

            if (ModelState.IsValid) {
                var participantes = await context.Participantes.ToListAsync ();
                var convidados = await context.Convidados.ToListAsync ();

                model.TotalArrecadado = (participantes.Count + convidados.Count) * valorPorPessoa;

                return model;
            } else {
                return BadRequest (ModelState);
            }
        }
    }
}