using System.Collections.Generic;
using System;

namespace SalesManagement.Application.DTOs
{
    public class VendaDTO
    {
        public Guid ClienteId { get; set; } 
        public List<ItemVendaDTO> Itens { get; set; } = new();
    }

    public class ItemVendaDTO
    {
        public Guid ProdutoId { get; set; } 
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}

