using System.Collections.Generic;

namespace BetEventScanner.DataAccess.Providers
{
    public interface IDbProvider
    {
        void GetEntity<T>(string collectionName, int id);

        void GetEntities<T>(string collectionName);

        void InsertEntity<T>(T entity);

        void InsertEntities<T>(IEnumerable<T>  entities);

        void UpdateEntity<T>(T entity);

        void DeleteEntity(int id);
    }
}