using Microsoft.AspNetCore.Mvc;
using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;
using System;

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
		public IActionResult GetVendas()
		{
			var vendas = _vendaService.ListarVendas();
			return Ok(vendas);
		}

		[HttpPost]
		public IActionResult CriarVenda([FromBody] VendaDTO vendaDto)
		{
			_vendaService.CriarVenda(vendaDto);
			return Ok("Venda criada com sucesso!");
		}

		[HttpDelete("{id}")]
		public IActionResult CancelarVenda(Guid id)
		{
			_vendaService.CancelarVenda(id);
			return NoContent();
		}

		[HttpPut("{id}")]
		public IActionResult AtualizarVenda(Guid id, [FromBody] VendaDTO vendaDto)
		{
			try
			{
				_vendaService.AtualizarVenda(id, vendaDto);
				return Ok("Venda atualizada com sucesso!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
