namespace Foundation.Core.Types
{
    public class BaseCollection<TEntity> : List<TEntity> where TEntity : class
    {
        protected BaseCollection()
        {
            
        }
        protected BaseCollection(IEnumerable<TEntity> entities)
        {
            AddRange(entities);
        }
    }
}