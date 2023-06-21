namespace Foundation.Core.Interfaces
{
    public interface IBaseEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }
    }
}