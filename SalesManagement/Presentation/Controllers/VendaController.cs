using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;

namespace SalesManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/vendas")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _vendaService;

        public VendaController(VendaService vendaService)
        {
            _vendaService = vendaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendas()
        {
            var vendas = await _vendaService.ListarVendasAsync();
            return Ok(vendas);
        }

        [HttpPost]
        public async Task<IActionResult> CriarVenda([FromBody] VendaDTO vendaDto)
        {
            await _vendaService.CriarVendaAsync(vendaDto);
            return Ok("Venda criada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelarVenda(Guid id)
        {
            await _vendaService.CancelarVendaAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVenda(Guid id, [FromBody] VendaDTO vendaDto)
        {
            try
            {
                await _vendaService.AtualizarVendaAsync(id, vendaDto);
                return Ok("Venda atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


