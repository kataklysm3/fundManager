namespace FundManager.Impl.Domain.Contracts
{
    public class FundId : IIdentity
    {
        public FundId(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}