using System;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Commands;
using FundManager.Impl.Storage;

namespace FundManager.Impl.Domain.FundAggregate
{
    public class FundManagerService : ICommandsHandlingService
    {
        private readonly IEventStorage _eventStorage;

        public FundManagerService(IEventStorage eventStorage)
        {
            _eventStorage = eventStorage;
        }

        public void Execute(ICommand cmd)
        {
            ((dynamic) this).When((dynamic) cmd);
        }

        public void When(CreateFundCommand c)
        {
            Update(c.Id, a => a.Create(c.Name, DateTime.UtcNow));
        }

        public void When(AddStockCommand c)
        {
            Update(c.FundId, a => a.AddStock(c.StockName, c.Price, c.Quantity, DateTime.UtcNow));
        }

        private void Update(FundId id, Action<Fund> executeAction)
        {
            var eventStream = _eventStorage.LoadEventStream(id);
            var fund = new Fund(eventStream.Events);
            executeAction(fund);
            _eventStorage.AppendToStream(id, eventStream.Version, fund.Changes);
        }
    }
}