namespace Foundation.Core.Interfaces
{
    public interface IMap<in TFrom, out TTo>
    {
        TTo Map(TFrom from);
        IEnumerable<TTo> Map(IEnumerable<TFrom> from);
    }
}