using SalesManagement.Application.DTOs;
using SalesManagement.Application.Services;
using SalesManagement.Domain.Entities;
using SalesManagement.Infrastructure.Persistence.Repositories;

namespace SalesManagement.Tests
{
    public class VendaServiceTests
    {
        private readonly Mock<VendaRepository> _mockRepository;
        private readonly VendaService _vendaService;

        public VendaServiceTests()
        {
            _mockRepository = new Mock<VendaRepository>(null);
            _vendaService = new VendaService(_mockRepository.Object);
        }

        [Fact]
        public async Task CriarVendaAsync_DeveAdicionarVenda()
        {
            // Arrange
            var vendaDto = new VendaDTO
            {
                ClienteId = Guid.NewGuid(),
                Itens = new List<ItemVendaDTO>
                {
                    new ItemVendaDTO
                    {
                        ProdutoId = Guid.NewGuid(),
                        Quantidade = 5,
                        PrecoUnitario = 10.0m
                    }
                }
            };

            // Act
            await _vendaService.CriarVendaAsync(vendaDto);

            // Assert
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Venda>()), Times.Once);
        }
    }
}
