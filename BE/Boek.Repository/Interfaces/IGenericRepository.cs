using System.Linq.Expressions;

namespace Boek.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetQuery();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get<TKey>(TKey id);
        Task<TEntity> GetAsync<TKey>(TKey id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        void UpdateRange(IEnumerable<TEntity> entity);
        void RemoveRange(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        void Delete<TKey>(TKey id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync<TKey>(TKey id);
    }
}
