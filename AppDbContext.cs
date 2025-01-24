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

            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Itens)
                .WithOne()
                .HasForeignKey("VendaId");
        }
    }
}
