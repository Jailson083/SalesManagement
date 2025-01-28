using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relação entre Venda e ItemVenda
            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Itens)
                .WithOne()  // Sem navegação reversa explícita
                .HasForeignKey("VendaId")  // Define a chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Se a venda for excluída, os itens também serão

            // Configuração da entidade ItemVenda para garantir compatibilidade com EF
            modelBuilder.Entity<ItemVenda>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd(); // Garante que o Id seja gerado automaticamente
        }
    }
}

