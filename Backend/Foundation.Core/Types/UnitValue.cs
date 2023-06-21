namespace Foundation.Core.Types
{
    public class UnitValue<T>
    {
        public UnitValue(T value, string unit)
        {
            Value = value;
            Unit = unit;
        }
        
        public T Value { get; set; }
        public string Unit { get; set; }
    }
}