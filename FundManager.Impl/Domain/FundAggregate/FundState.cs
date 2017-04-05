using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManager.Impl.Domain.FundAggregate
{
    class FundState
    {
        public  bool Created { get; private set; }

        public FundState(IEnumerable<IEvent> events)
        {
            
        }

        public void Mutate(IEvent e)
        {
            
        }
    }
}
