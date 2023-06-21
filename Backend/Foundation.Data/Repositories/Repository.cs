using System.Linq.Expressions;
using Foundation.Core.Types;
using Foundation.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Set;
    
        public Repository(DbContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException();
            Set = dbContext.Set<TEntity>();
        }
    
        public void Dispose()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Queryable(false).Where(predicate).AnyAsync(cancellationToken);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, 
            bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            return await Queryable(enableTracking).Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ICollection<TEntity>> GetListAsync(bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            return await Queryable(enableTracking).ToListAsync(cancellationToken);
        }

        public async Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, 
            bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            return await Queryable(enableTracking).Where(predicate).ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> Queryable(bool enableTracking = false)
        {
            return enableTracking ? Set : Set.AsNoTracking();
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Set.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            Set.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Set.UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Set.RemoveRange(entities);
        }
    }
}