using System;

namespace FundManager.Impl.Domain.Contracts
{
    public class StockId: IIdentity
    {
        public int Id { get; }

        public StockId(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}