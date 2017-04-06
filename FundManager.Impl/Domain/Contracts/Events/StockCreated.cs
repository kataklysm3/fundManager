using System;

namespace FundManager.Impl.Domain.Contracts.Events
{
    public class StockCreated: IEvent
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public StockId Id { get; set; }

        public override string ToString()
        {
            return $"Stock {Name} Created on {Created}";
        }
    }
}