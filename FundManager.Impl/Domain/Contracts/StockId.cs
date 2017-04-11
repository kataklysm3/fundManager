using System;

namespace FundManager.Impl.Domain.Contracts
{
    public class StockId: IIdentity
    {
        public Guid Id { get; }

        public StockId(Guid id)
        {
            Id = id;
        }
    }
}