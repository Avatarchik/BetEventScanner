using System.Collections.Generic;

namespace BetEventScanner.DataAccess.Contracts
{
    public interface IDbProvider
    {
        T Get<T>(string collectionName, int id);

        IEnumerable<T> GetMany<T>(string collectionName);

        void Insert<T>(string collectionName, T entity);

        void InsertMany<T>(string collectionName, IEnumerable<T>  entities);

        void Update<T>(string collectionName, T entity);

        void Delete(int id);
    }
}