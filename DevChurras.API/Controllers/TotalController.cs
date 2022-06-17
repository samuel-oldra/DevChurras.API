using DevChurras.API.Data;
using DevChurras.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevChurras.API.Controllers
{
    [ApiController]
    [Route("api/total")]
    public class TotalController : ControllerBase
    {
        // POST: api/total
        /// <summary>
        /// Cálculo dos Totais
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "valorGastoComida": 150,
        ///     "valorGastoBebida": 60
        /// }
        /// </remarks>
        /// <param name="context">DataContext</param>
        /// <param name="model">Dados dos Valores</param>
        /// <returns>Objeto criado</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] Total model)
        {
            var valorPorPessoa = 40;
            var valorPorPessoaQueNaoBebe = 30;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var participantes = await context.Participantes.ToListAsync();

            foreach (var participante in participantes)
            {
                if (participante.ConsomeBebidaAlcoolica)
                    model.IncrementaValorArrecadado(valorPorPessoa);
                else
                    model.IncrementaValorArrecadado(valorPorPessoaQueNaoBebe);
            }

            var convidados = await context.Convidados.ToListAsync();

            foreach (var convidado in convidados)
            {
                if (convidado.ConsomeBebidaAlcoolica)
                    model.IncrementaValorArrecadado(valorPorPessoa);
                else
                    model.IncrementaValorArrecadado(valorPorPessoaQueNaoBebe);
            }

            return Ok(model);
        }
    }
}