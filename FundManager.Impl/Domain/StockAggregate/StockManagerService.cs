using System;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Commands;
using FundManager.Impl.Storage;

namespace FundManager.Impl.Domain.StockAggregate
{
    public class StockManagerService : ICommandsHandlingService
    {
        private readonly IEventStorage _eventStorage;

        public StockManagerService(IEventStorage eventStorage)
        {
            _eventStorage = eventStorage;
        }

        public void Execute(ICommand cmd)
        {
            ((dynamic) this).When((dynamic) cmd);
        }

        public void When(CreateStockCommand cmd)
        {
            Update(cmd.Id, s => s.Create(cmd.Id, cmd.Name, DateTime.Now));
        }

        private void Update(StockId id, Action<Stock> executeAction)
        {
            var stream = _eventStorage.LoadEventStream(id);
            var stock = new Stock(stream.Events);
            executeAction(stock);
            _eventStorage.AppendToStream(id, stream.Version, stock.Changes);
        }
    }
}