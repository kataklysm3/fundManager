using System.Globalization;

namespace FundManager.Impl.Domain.Contracts
{
    /// <summary>
    /// Price of the stock entity,
    /// currency can be added later
    /// </summary>
    public struct StockPrice
    {
        public decimal Amount { get; set; }

        public StockPrice(decimal amount)
        {
            Amount = amount;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}