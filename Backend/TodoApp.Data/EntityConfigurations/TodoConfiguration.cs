using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.EntityConfigurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(255);
            
            builder.OwnsOne(t => t.Deadline, tf =>
            {
                tf.Property(d => d.From).HasColumnName("DeadlineFrom");
                tf.Property(d => d.To).HasColumnName("DeadlineTo");
            });
        }
    }
}