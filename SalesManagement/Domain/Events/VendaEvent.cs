using System;

namespace SalesManagement.Domain.Events
{
    public abstract class VendaEvent
    {
        public Guid VendaId { get; private set; }
        public DateTime Timestamp { get; private set; }

        protected VendaEvent(Guid vendaId)
        {
            VendaId = vendaId;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class SaleCreatedEvent : VendaEvent
    {
        public SaleCreatedEvent(Guid vendaId) : base(vendaId) { }
    }

    public class SaleModifiedEvent : VendaEvent
    {
        public SaleModifiedEvent(Guid vendaId) : base(vendaId) { }
    }

    public class SaleCancelledEvent : VendaEvent
    {
        public SaleCancelledEvent(Guid vendaId) : base(vendaId) { }
    }

    public class ItemCancelledEvent : VendaEvent
    {
        public Guid ItemId { get; private set; }

        public ItemCancelledEvent(Guid vendaId, Guid itemId) : base(vendaId)
        {
            ItemId = itemId;
        }
    }
}
