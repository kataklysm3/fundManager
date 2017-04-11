using System;
using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;

namespace FundManager.Impl.Domain.FundAggregate
{
    public class Fund
    {
        public readonly IList<IEvent> Changes = new List<IEvent>();

        private readonly FundState _state;

        public Fund(IEnumerable<IEvent> events)
        {
            _state = new FundState(events);
        }

        internal void Apply(IEvent e)
        {
            _state.Mutate(e);
            Changes.Add(e);
        }

        public void Create(string name, DateTime timeAdded)
        {
            if(_state.Created)
                throw new InvalidOperationException("Fund was already created");

            Apply(new FundCreated()
            {
                Id = _state.FundId,
                Created = timeAdded,
                Name = name
            });
        }

        public void AddStock(string name, StockPrice price, int quantity, DateTime timeAdded)
        {
            Apply(new StockAdded()
            {
                Id = new StockId(new Guid()),
                StockName = name,
                Price = price,
                Quantity = quantity,
                TimeAdded = timeAdded,
                Transaction = _state.MaxTransactionId + 1
            });
        }
    }
}