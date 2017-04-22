namespace FundManager.Impl.Domain.Contracts
{
    public class FundId : IIdentity
    {
        public FundId(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var fooItem = obj as FundId;

            return fooItem.Id == Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
    }
}