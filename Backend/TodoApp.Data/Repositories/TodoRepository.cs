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
        public async Task<Todo?> GetByIdAsync(Guid id, bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            return await GetAsync(e => e.Id == id, enableTracking: enableTracking, cancellationToken: cancellationToken);
        }
    }
}