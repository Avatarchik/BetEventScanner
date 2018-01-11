using System.Collections.Generic;

namespace BetEventScanner.Providers.Contracts
{
    public interface IDataSource<T>
    {
        ICollection<T> GetSourceData();
    }
}