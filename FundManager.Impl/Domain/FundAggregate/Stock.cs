using FundManager.Impl.Domain.Contracts;

namespace FundManager.Impl.Domain.FundAggregate
{
    public class Stock
    {
        public string Name { get; set; }
        public StockId StockId { get; set; }
        public StockPrice Price { get; set; }
        public int Quantity { get; set; }
    }
}