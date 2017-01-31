using System;
using System.Collections.Generic;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class OddsRepository<T> : IRepository<T>
    {
        private readonly IDbProvider _dbProvider = new MongoDbProvider();
        private readonly string _collectionName = "Odds";

        public T GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetEntities()
        {
            throw new NotImplementedException();
        }

        public void InsertEntity(T entity)
        {
            _dbProvider.InsertEntity(_collectionName, entity);
        }

        public void InsertEntities(IEnumerable<T> entities)
        {
            _dbProvider.InsertEntities(_collectionName, entities);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }

    public interface IRepository<T>
    {
        T GetEntityById(int id);

        IEnumerable<T> GetEntities();

        void InsertEntity(T entity);

        void InsertEntities(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(int id);
    }
}