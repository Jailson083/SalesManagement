using SalesManagement.Domain.Entities;

public class Venda
{
    public Guid Id { get; private set; }
    public DateTime DataVenda { get; private set; }
    public Guid ClienteId { get; private set; }
    public List<ItemVenda> Itens { get; private set; } = new();
    public decimal ValorTotal => Itens.Sum(i => i.ValorTotal);
    public bool Cancelada { get; private set; }

    private Venda() { }

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

    public void AtualizarItens(List<ItemVenda> novosItens)
    {
        if (novosItens == null || !novosItens.Any())
            throw new ArgumentException("A venda deve ter pelo menos um item.");

        if (novosItens.Any(i => i.Quantidade > 20))
            throw new ArgumentException("Não é permitido vender mais de 20 itens idênticos.");

        Itens = novosItens; // Atualiza os itens da venda
    }

}
