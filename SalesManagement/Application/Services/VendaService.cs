using SalesManagement.Application.DTOs;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Application.Services
{
    public class VendaService
    {
        private readonly VendaRepository _repository;

        public VendaService(VendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Venda>> ListarVendasAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CriarVendaAsync(VendaDTO vendaDto)
        {
            var venda = new Venda(
                clienteId: vendaDto.ClienteId,
                itens: vendaDto.Itens.Select(i => new ItemVenda(
                    produtoId: i.ProdutoId,
                    quantidade: i.Quantidade,
                    precoUnitario: i.PrecoUnitario
                )).ToList()
            );

            await _repository.AddAsync(venda);
        }

        public async Task AtualizarVendaAsync(Guid id, VendaDTO vendaDto)
        {
            var venda = await _repository.GetByIdAsync(id);
            if (venda == null) throw new Exception("Venda não encontrada.");

            venda.AtualizarItens(vendaDto.Itens.Select(i => new ItemVenda(
                produtoId: i.ProdutoId,
                quantidade: i.Quantidade,
                precoUnitario: i.PrecoUnitario
            )).ToList());

            await _repository.UpdateAsync(venda);
        }

        public async Task CancelarVendaAsync(Guid id)
        {
            var venda = await _repository.GetByIdAsync(id);
            if (venda == null) throw new Exception("Venda não encontrada.");

            venda.Cancelar();
            await _repository.UpdateAsync(venda);
        }
    }
}
