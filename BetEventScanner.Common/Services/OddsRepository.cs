using System;
using System.Collections.Generic;
using BetEventScanner.Common.Contracts;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.Common.Services
{
    public class OddsRepository<T> : IRepository<T>
    {
        private readonly IDbProvider _dbProvider = new MongoDbProvider("footballdb");
        private readonly string _collectionName = "Odds";

        public T GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetEntities()
        {
            return _dbProvider.GetEntities<T>(_collectionName);
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
}