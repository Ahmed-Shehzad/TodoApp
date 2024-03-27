using Foundation.Data.Repositories;
using TodoApp.Data.Context;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Repositories
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Gets a Todo entity with the specified ID from the data store.
        /// </summary>
        /// <param name="id">The ID of the Todo entity to retrieve.</param>
        /// <param name="enableTracking">A value indicating whether to enable change tracking for the query.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing the retrieved Todo entity or null if no entity was found.</returns>
        public async Task<Todo?> GetByIdAsync(Guid id, bool enableTracking = false,
            CancellationToken cancellationToken = default)
        {
            return await GetAsync(e => e.Id == id, enableTracking: enableTracking,
                cancellationToken: cancellationToken);
        }
    }
}