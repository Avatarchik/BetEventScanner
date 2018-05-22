using BetEventScanner.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace BetEventScanner.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;
        private bool _disposed;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Returns repository as queryable object
        /// </summary>
        /// <returns>Queryable decorator</returns>
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        /// <summary>
        /// Returns repository as queryable object without tracking in EF cache
        /// </summary>
        /// <returns>Queryable decorator</returns>
        public virtual IQueryable<TEntity> AsQueryableNotTracking()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// Returns all entries in table
        /// </summary>
        /// <returns>Set of entities</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Returns all entries in a table asynchronous
        /// </summary>
        /// <returns>Set of entities</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Getting an entity by Id 
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Requested entity</returns>
        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Getting an entityby Id asynchronous
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Requested entity</returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Creating entity
        /// </summary>
        /// <param name="entity">Entity for creating </param>
        public virtual void Create(TEntity entity)
        {
            DbEntityEntry entry = _context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(entity);
            }
        }

        /// <summary>
        /// Creating bunch of entities
        /// </summary>
        /// <param name="entities">Entities for creating in the database</param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Updating entity
        /// </summary>
        /// <param name="entity">Entity for updating (has to have Id)</param>
        public virtual void Update(TEntity entity)
        {
            DbEntityEntry entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void AddOrUpdate(IEntity entity)
        {
            if (entity.Id == 0)
            {
                Create(entity as TEntity);
            }
            else
            {
                Update(entity as TEntity);
            }
        }

        /// <summary>
        /// Deleting entity
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null) _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deleting entity asynchronous
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
