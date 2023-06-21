using System.Reflection;
using Foundation.Data.Types;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Context
{
    public class TodoDbContext : BaseDbContext<TodoDbContext>
    {
        public DbSet<Todo> Todos { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}