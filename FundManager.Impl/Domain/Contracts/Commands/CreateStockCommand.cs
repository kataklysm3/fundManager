namespace FundManager.Impl.Domain.Contracts.Commands
{
    public class CreateStockCommand : ICommand
    {
        public string Name { get; set; }
        public StockId Id { get; set; }
        public FundId FundId { get; set; }

        public override string ToString()
        {
            return $"Create Stock {Name} in Fund {FundId}";
        }
    }
}