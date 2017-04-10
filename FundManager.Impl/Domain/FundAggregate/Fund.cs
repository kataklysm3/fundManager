using System;
using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;
using FundManager.Impl.Domain.StockAggregate;

namespace FundManager.Impl.Domain.FundAggregate
{
    public class Fund: IChildEntitiesAware<IEvent>
    {
        public readonly IList<IEvent> Changes = new List<IEvent>();

        private readonly FundState _state;

        private readonly EntityList<Stock, IEvent> _stocks;

        public Fund(IEnumerable<IEvent> events)
        {
            _stocks = new EntityList<Stock, IEvent>(this);

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
                FundId = _state.FundId,
                StockName = name,
                Price = price,
                Quantity = quantity,
                TimeAdded = timeAdded,
                Transaction = _state.MaxTransactionId + 1
            });
        }

        public void RegisterChildEventProvider(IEntityEventProvider<IEvent> entityEventProvider)
        {
            throw new NotImplementedException();
        }
    }
}