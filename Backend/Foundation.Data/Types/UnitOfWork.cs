using Foundation.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Data.Types
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }
        
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}