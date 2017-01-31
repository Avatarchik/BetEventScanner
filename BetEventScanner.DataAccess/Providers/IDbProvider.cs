using System.Collections.Generic;

namespace BetEventScanner.DataAccess.Providers
{
    public interface IDbProvider
    {
        T GetEntity<T>(string collectionName, int id);

        IEnumerable<T> GetEntities<T>(string collectionName);

        void InsertEntity<T>(string collectionName, T entity);

        void InsertEntities<T>(string collectionName, IEnumerable<T>  entities);

        void UpdateEntity<T>(T entity);

        void DeleteEntity(int id);
    }
}