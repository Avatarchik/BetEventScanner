using System;
using System.Collections.Generic;
using BetEventScanner.DataAccess.Contracts;
using BetEventScanner.DataAccess.Providers;

namespace BetEventScanner.DataAccess.Mongo
{
    public class MongoRepository<T> : IRepository<T> where T : IDocEntity
    {
        private readonly string _collectionName;
        private readonly IDbProvider _sourceDbProvider;

        public MongoRepository(string collectionName)
        {
            _collectionName = collectionName;
            _sourceDbProvider = new MongoDbProvider("footballdb");
        }

        public T GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetEntities()
        {
            return _sourceDbProvider.GetEntities<T>(_collectionName);
        }

        public void InsertEntity(T entity)
        {
            _sourceDbProvider.InsertEntity(_collectionName, entity);
        }

        public void InsertEntities(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            _sourceDbProvider.UpdateEntity(_collectionName, entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
