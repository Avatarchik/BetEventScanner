using System.Collections.Generic;

namespace BetEventScanner.Providers.Contracts
{
    public interface IStorage
    {
        void Store<T>(T data);
        void Store<T>(ICollection<T> data);
    }
}
