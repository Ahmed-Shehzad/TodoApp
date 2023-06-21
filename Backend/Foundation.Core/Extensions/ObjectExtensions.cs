using System.Text.Json;

namespace Foundation.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Easy System.Text.Json serialization
        /// </summary>
        /// <param name="element"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string ToString(this object element, JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(element, options);
        }
        
        public static IEnumerable<T> Yield<T>(this T @this)
        {
            yield return @this;
        }
    }
}