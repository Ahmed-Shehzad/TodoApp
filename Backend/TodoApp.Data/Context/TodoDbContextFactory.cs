using Foundation.Data.Extensions;
using Foundation.Data.Types;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data.Context
{
    public class TodoDbContextFactory : DesignTimeDbContextFactory<TodoDbContext>
    {
        protected override TodoDbContext CreateNewInstance(DbContextOptions<TodoDbContext> options)
        {
            return new TodoDbContext(options);
        }
        protected override void ConfigureOptions(string? connectionString, DbContextOptionsBuilder<TodoDbContext> optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(GetType().Assembly.ToString());
                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", DbContextExtensions.GetDefaultSchema<TodoDbContext>());
            });
        }
    }
}