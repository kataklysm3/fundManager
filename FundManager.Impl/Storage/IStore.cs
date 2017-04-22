using System.Collections.Generic;

namespace FundManager.Impl.Storage
{
    public interface IStore
    {
        void Append(string streamName, byte[] data, long expectedStreamVersion = -1);
        IEnumerable<DataWithVersion> ReadRecords(string streamName, long afterVersion, int maxCount);
        IEnumerable<DataWithName> ReadRecords(long afterVersion, int maxCount);
        void Close();
    }
}