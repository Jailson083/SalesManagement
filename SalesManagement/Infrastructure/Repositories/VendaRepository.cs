using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Infrastructure.Persistence.Repositories
{
    public class VendaRepository
    {
        private readonly AppDbContext _context;

        public VendaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task<Venda> GetByIdAsync(Guid id)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Venda>> GetAllAsync()
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                .ToListAsync();
        }

        public async Task UpdateAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Venda venda)
        {
            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
        }
    }
}
