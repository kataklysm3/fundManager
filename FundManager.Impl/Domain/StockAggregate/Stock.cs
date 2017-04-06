using System;
using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;

namespace FundManager.Impl.Domain.StockAggregate
{
    public class Stock
    {
        private readonly StockState _state;
        public readonly IList<IEvent> Changes = new List<IEvent>();

        public Stock(IEnumerable<IEvent> events)
        {
            _state = new StockState(events);
        }

        private void Apply(IEvent e)
        {
            _state.Mutate(e);
            Changes.Add(e);
        }

        public void Create(StockId id, string name, DateTime timeCreated)
        {
            if (_state.Created)
                throw new InvalidOperationException("Stock was already created");

            Apply(new StockCreated
            {
                Id = id,
                Name = name,
                Created = timeCreated
            });
        }
    }
}