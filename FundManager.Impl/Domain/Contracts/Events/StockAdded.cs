using System;

namespace FundManager.Impl.Domain.Contracts.Events
{
    [Serializable]
    public class StockAdded : IEvent
    {
        public StockId Id { get; set; }
        public string StockName { get; set; }
        public StockPrice Price { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeAdded { get; set; }
        public int Transaction { get; set; }

        public override string ToString()
        {
            return $"Added '{StockName}' with Id '{Id}' Price = {Price}";
        }
    }
}