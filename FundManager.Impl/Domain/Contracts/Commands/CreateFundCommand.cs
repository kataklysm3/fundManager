using System;

namespace FundManager.Impl.Domain.Contracts.Commands
{
    [Serializable]
    public class CreateFundCommand : ICommand
    {
        public FundId Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Created Fund {Name} id {Id}";
        }
    }
}