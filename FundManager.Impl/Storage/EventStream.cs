using System.Collections.Generic;

namespace FundManager.Impl.Storage
{
    public class EventStream
    {
        public List<IEvent> Events = new List<IEvent>();
        public long Version;
    }
}