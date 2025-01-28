using System;

namespace SalesManagement.Domain.Entities
{
    public class ItemVenda
    {
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal => (PrecoUnitario * Quantidade) - Desconto;

        private ItemVenda() { }

        public ItemVenda(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
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

