using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace FundManager.Impl.Storage
{
    public class EventStorage : IEventStorage
    {
        private readonly IStore _dataStore;
        
        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        public EventStorage(IStore dataStore)
        {
            _dataStore = dataStore;
        }

        public EventStream LoadEventStream(IIdentity id)
        {
            return LoadEventStream(id, 0, int.MaxValue);
        }

        public EventStream LoadEventStream(IIdentity id, long skip, int take)
        {
            var name = id.ToString(); //default solution
            var records = _dataStore.ReadRecords(name, skip, take).ToList();
            var stream = new EventStream();

            foreach (var tapeRecord in records)
            {
                stream.Events.AddRange(DeserializeEvent(tapeRecord.Data));
                stream.Version = tapeRecord.Version;
            }
            return stream;
        }

        public void AppendToStream(IIdentity id, long expectedVersion, ICollection<IEvent> events)
        {
            if (events.Count == 0)
                return;
            var name = id.ToString();
            var data = SerializeEvent(events.ToArray());

            _dataStore.Append(name, data, expectedVersion);

            foreach (var @event in events)
                Debug.WriteLine("  {0} r{1} Event: {2}", id, expectedVersion, @event);
        }

        private byte[] SerializeEvent(IEvent[] e)
        {
            using (var mem = new MemoryStream())
            {
                _formatter.Serialize(mem, e);
                return mem.ToArray();
            }
        }

        private IEvent[] DeserializeEvent(byte[] data)
        {
            using (var mem = new MemoryStream(data))
            {
                return (IEvent[]) _formatter.Deserialize(mem);
            }
        }
    }
}