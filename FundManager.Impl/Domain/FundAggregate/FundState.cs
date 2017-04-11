using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;

namespace FundManager.Impl.Domain.FundAggregate
{
    internal class FundState
    {
        private readonly IList<Stock> _stocks;

        public FundState(IEnumerable<IEvent> events)
        {
            _stocks = new List<Stock>();
            foreach (var e in events)
            {
                Mutate(e);
            }
        }

        public string FundName { get; private set; }
        public FundId FundId { get; private set; }
        public bool Created { get; private set; }
        public int MaxTransactionId { get; private set; }

        public void When(FundCreated e)
        {
            Created = true;
            FundName = e.Name;
            FundId = e.Id;
        }

        public void When(StockAdded e)
        {
            _stocks.Add(new Stock()
            {
                Name = e.StockName,
                StockId = e.Id,
                Price = e.Price,
                Quantity = e.Quantity
            });
            MaxTransactionId = e.Transaction;
        }

        public void Mutate(IEvent e)
        {
            ((dynamic) this).When((dynamic) e);
        }
    }
}