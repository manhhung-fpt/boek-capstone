using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Boek.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BoekCapstoneContext _context;
        protected readonly DbSet<T> dbSet;

        public GenericRepository(BoekCapstoneContext context)
        {
            this._context = context;
            this.dbSet = context.Set<T>();
        }

        #region Gets
        public IQueryable<T> Get()
        {
            return dbSet;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public T Get<TKey>(TKey id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetAsync<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return dbSet.AsQueryable();
        }
        #endregion

        #region CRUD
        public void AddRange(IEnumerable<T> entity)
        {
            dbSet.AddRange(entity);
        }

        public void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public void Delete<TKey>(TKey id)
        {
            T entity = dbSet.Find(id);
            Delete(entity);
        }
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entity)
        {
            dbSet.UpdateRange(entity);
        }

        void IGenericRepository<T>.RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
        #endregion

        #region CRUD Async
        public async Task UpdateAsync(T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }

        public async Task DeleteAsync<TKey>(TKey id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                EntityEntry entityEntry = _context.Entry<T>(entity);
                entityEntry.State = EntityState.Deleted;
            }
            else
                throw new Exception("Cannot find entity by id");
        }
        #endregion
    }
}
