using Foundation.Data.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
        Task<Todo?> GetByIdAsync(Guid id, bool enableTracking = false, CancellationToken cancellationToken = default);
    }
}