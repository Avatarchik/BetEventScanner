using System.Collections.Generic;

namespace BetEventScanner.DataAccess.Contracts
{
    public interface IDbProvider
    {
        T GetEntity<T>(string collectionName, int id);

        IEnumerable<T> GetEntities<T>(string collectionName);

        void InsertEntity<T>(string collectionName, T entity);

        void InsertEntities<T>(string collectionName, IEnumerable<T>  entities);

        void UpdateEntity<T>(string collectionName, T entity) where T : IDocEntity;

        void DeleteEntity(int id);
    }
}