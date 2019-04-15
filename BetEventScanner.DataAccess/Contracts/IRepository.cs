using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetEventScanner.DataAccess.Contracts
{
    public interface IRepository<TEntity>
    {
        //IQueryable<TEntity> AsQueryable();

        ///// <summary>
        ///// Returns repository as queryable object without tracking in EF cache
        ///// </summary>
        ///// <returns>Queryable decorator</returns>
        //IQueryable<TEntity> AsQueryableNotTracking();

        ///// <summary>
        ///// Returns all entries in a table
        ///// </summary>
        ///// <returns>Set of entities</returns>
        //IEnumerable<TEntity> GetAll();

        ///// <summary>
        ///// Returns all entries in a table asynchronous
        ///// </summary>
        ///// <returns>Set of entities</returns>
        //Task<IEnumerable<TEntity>> GetAllAsync();

        ///// <summary>
        ///// Getting an entity by Id 
        ///// </summary>
        ///// <param name="id">id of the entity</param>
        ///// <returns>Requested entity</returns>
        //TEntity GetById(int id);

        ///// <summary>
        ///// Getting an entity by Id asynchronous
        ///// </summary>
        ///// <param name="id">Database id of the entity</param>
        ///// <returns>Requested entity</returns>
        //Task<TEntity> GetByIdAsync(int id);

        ///// <summary>
        ///// Creating entity
        ///// </summary>
        ///// <param name="entity">Entity for creating </param>
        //void Create(TEntity entity);

        ///// <summary>
        ///// Creating bunch of entities
        ///// </summary>
        ///// <param name="entities">Entities for creating</param>
        //void AddRange(IEnumerable<TEntity> entities);

        ///// <summary>
        ///// Updating entity
        ///// </summary>
        ///// <param name="entity">Entity for updating (has to have Id)</param>
        //void Update(TEntity entity);

        ///// <summary>
        ///// Updating entity or Creating if it's id is 0
        ///// </summary>
        ///// <param name="enity"></param>
        ///// <returns>Entity Id</returns>
        //void AddOrUpdate(IEntity enity);

        ///// <summary>
        ///// Deleting entity
        ///// </summary>
        ///// <param name="id">Id of the entity</param>
        //void Delete(int id);

        ///// <summary>
        ///// Deleting entity asynchronous
        ///// </summary>
        ///// <param name="id">Id of the entity</param>
        //Task DeleteAsync(int id);
    }
}