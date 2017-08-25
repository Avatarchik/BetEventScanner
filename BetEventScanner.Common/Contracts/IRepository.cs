using System.Collections.Generic;

namespace BetEventScanner.Common.Contracts
{
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