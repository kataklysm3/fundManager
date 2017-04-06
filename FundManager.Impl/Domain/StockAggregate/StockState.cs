using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;

namespace FundManager.Impl.Domain.StockAggregate
{
    public class StockState
    {
        public string Name { get; private set; }
        public bool Created { get; private set; }
        public StockId StockId { get; private set; }
        public int MaxTransactionId { get; private set; }

        public StockState(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                Mutate(e);
            }
        }

        public void When(StockCreated e)
        {
            Created = true;
            Name = e.Name;
            StockId = e.Id;
        }

        public void Mutate(IEvent e)
        {
            ((dynamic) this).When((dynamic) e);
        }
    }
}