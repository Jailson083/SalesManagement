using System.Collections.Generic;
using System;

namespace SalesManagement.Domain.Entities
{
    public class Venda
    {
        public Guid Id { get; private set; }
        public DateTime DataVenda { get; private set; }
        public Guid ClienteId { get; private set; }
        public List<ItemVenda> Itens { get; private set; } = new();
        public decimal ValorTotal => Itens.Sum(i => i.ValorTotal);
        public bool Cancelada { get; private set; }

        public Venda(Guid clienteId, List<ItemVenda> itens)
        {
            Id = Guid.NewGuid();
            DataVenda = DateTime.UtcNow;
            ClienteId = clienteId;
            Itens = itens;

            Validar();
        }

        private void Validar()
        {
            if (!Itens.Any()) throw new ArgumentException("A venda deve ter pelo menos um item.");
            if (Itens.Any(i => i.Quantidade > 20)) throw new ArgumentException("Não é permitido vender mais de 20 itens idênticos.");
        }

        public void Cancelar()
        {
            Cancelada = true;
        }
    }

    public class ItemVenda
    {
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal => (PrecoUnitario * Quantidade) - Desconto;

        public ItemVenda(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Desconto = CalcularDesconto(quantidade);
        }

        private decimal CalcularDesconto(int quantidade)
        {
            if (quantidade >= 10 && quantidade <= 20) return PrecoUnitario * quantidade * 0.2m;
            if (quantidade >= 4) return PrecoUnitario * quantidade * 0.1m;
            return 0m;
        }
    }
}
