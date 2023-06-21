using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Data.Extensions
{
    public static class DbContextExtensions
    {
        private static string GetDefaultSchema(MemberInfo type) =>
            type.Name.Replace(nameof(DbContext),
                    string.Empty)
                .ToFirstLetterLowerCase();
        
        public static string GetDefaultSchema<TContext>() where TContext: DbContext
        {
            return GetDefaultSchema(typeof(TContext));
        }
        
        public static string GetDefaultSchema(this DbContext context)
        {
            return GetDefaultSchema(context.GetType());
        }
    }
}