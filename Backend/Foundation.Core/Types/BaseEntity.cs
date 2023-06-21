using Foundation.Core.Interfaces;

namespace Foundation.Core.Types
{
    public class BaseEntity : IBaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime CreatedAt { get; }
    }
}