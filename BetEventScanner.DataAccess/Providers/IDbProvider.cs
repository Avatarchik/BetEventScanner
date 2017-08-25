using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;

namespace BetEventScanner.DataAccess.Providers
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