using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;
using FundManager.Impl.Domain.StockAggregate;

namespace FundManager.Impl.Domain.FundAggregate
{
    internal class FundState
    {
        public FundState(IEnumerable<IEvent> events)
        {
            foreach (var e in events)
            {
                Mutate(e);
            }
        }

        //public IList<Stock> Stocks { get; private set; }
        public string FundName { get; private set; }
        public FundId FundId { get; private set; }
        public bool Created { get; private set; }
        public int MaxTransactionId { get; private set; }

        public void When(FundCreated e)
        {
            Created = true;
            FundName = e.Name;
            FundId = e.Id;
            //Stocks = new List<Stock>();
        }

        public void When(StockAdded e)
        {
            MaxTransactionId = e.Transaction;
        }

        public void Mutate(IEvent e)
        {
            ((dynamic) this).When((dynamic) e);
        }
    }
}