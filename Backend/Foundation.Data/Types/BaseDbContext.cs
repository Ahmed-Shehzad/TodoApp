using Microsoft.EntityFrameworkCore;

namespace Foundation.Data.Types
{
    public class BaseDbContext<TContext> : DbContext where TContext: DbContext
    {
        protected BaseDbContext(DbContextOptions<TContext> options): base(options)
        {
        }
    }
}