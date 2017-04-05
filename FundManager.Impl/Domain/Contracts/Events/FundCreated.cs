using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManager.Impl.Domain.Contracts.Events
{
    public class FundCreated : IEvent
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public FundId Id { get; set; }
    }
}