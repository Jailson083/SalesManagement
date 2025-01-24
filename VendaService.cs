using SalesManagement.Application.DTOs;
using SalesManagement.Domain;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Events;
using System.Collections.Generic;
using System;

namespace SalesManagement.Application.Services
{
    public class VendaService
    {
        public List<Venda> Vendas { get; private set; } = new();

        public void CriarVenda(VendaDTO vendaDto)
        {
            var venda = new Venda(
                clienteId: vendaDto.ClienteId,
                itens: vendaDto.Itens.Select(i => new ItemVenda(
                    produtoId: i.ProdutoId,
                    quantidade: i.Quantidade,
                    precoUnitario: i.PrecoUnitario
                )).ToList()
            );

            Vendas.Add(venda);

            DomainEventDispatcher.Dispatch(new SaleCreatedEvent(venda.Id));
        }

        public void AtualizarVenda(Guid vendaId, VendaDTO vendaDto)
        {
            var vendaExistente = Vendas.FirstOrDefault(v => v.Id == vendaId);
            if (vendaExistente == null)
            {
                throw new Exception("Venda não encontrada.");
            }

            vendaExistente.Itens = vendaDto.Itens.Select(i => new ItemVenda(
                produtoId: i.ProdutoId,
                quantidade: i.Quantidade,
                precoUnitario: i.PrecoUnitario
            )).ToList();

            DomainEventDispatcher.Dispatch(new SaleModifiedEvent(vendaId));
        }

        public void CancelarVenda(Guid vendaId)
        {
            var venda = Vendas.FirstOrDefault(v => v.Id == vendaId);
            if (venda == null) throw new Exception("Venda não encontrada.");

            venda.Cancelar();

            DomainEventDispatcher.Dispatch(new SaleCancelledEvent(vendaId));
        }
    }
}

