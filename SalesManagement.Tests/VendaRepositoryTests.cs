using Microsoft.EntityFrameworkCore;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Persistence;
using SalesManagement.Infrastructure.Persistence.Repositories;

namespace SalesManagement.Tests
{
    public class VendaRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public VendaRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarVenda()
        {
            // Arrange
            using var context = new AppDbContext(_options);
            var repository = new VendaRepository(context);

            var venda = new Venda(Guid.NewGuid(), new List<ItemVenda>
            {
                new ItemVenda(Guid.NewGuid(), 5, 10.0m)
            });

            // Act
            await repository.AddAsync(venda);

            // Assert
            var vendaSalva = await context.Vendas.FirstOrDefaultAsync(v => v.Id == venda.Id);
            Assert.NotNull(vendaSalva);
        }
    }
}

