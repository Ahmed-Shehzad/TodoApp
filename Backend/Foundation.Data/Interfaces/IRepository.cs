using System.Linq.Expressions;
using Foundation.Core.Types;

namespace Foundation.Data.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> GetListAsync(bool enableTracking = false, CancellationToken cancellationToken = default); 
        Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false, CancellationToken cancellationToken = default); 
        IQueryable<TEntity> Queryable(bool disableTracking = false);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}