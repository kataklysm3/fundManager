using System.Collections.Generic;

namespace FundManager.Impl.Domain
{
    public interface IEntityEventProvider<TEvent>
        where TEvent: IEvent
    {
        //void Clear();
        //IEnumerable<IEvent> GetChanges();
        //void LoadFromHistory(IEnumerable<IEvent> events);
    }
}