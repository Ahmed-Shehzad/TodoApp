using System.Text.RegularExpressions;

namespace Foundation.Data.Extensions
{
    public static class BaseModelBuilderExtensions
    {
        public static string ToFirstLetterLowerCase(this string str)
        {
            return char.ToLower(str[0]) + str[1..];
        }
        
        public static string ToSnakeCase(this string str)
        {
            str = Regex.Replace(str,"((?!_).)([A-Z][a-z]+)", "$1_$2");
            return Regex.Replace(str,"([a-z0-9])([A-Z])","$1_$2").ToLower();
        }
    }
}