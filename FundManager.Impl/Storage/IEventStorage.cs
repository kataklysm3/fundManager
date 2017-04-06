using System.Collections.Generic;

namespace FundManager.Impl.Storage
{
    public interface IEventStorage
    {
        EventStream LoadEventStream(IIdentity id);
        EventStream LoadEventStream(IIdentity entity, long skip, int max);
        void AppendToStream(IIdentity id, long expectedVersion, ICollection<IEvent> events);
    }
}