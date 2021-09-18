using System.Collections.Generic;
using System.Linq;

namespace Extreme.Starry.Updater.Client.Utils
{
    internal static class QueryExtensions
    {
        public static string GetQueryString(this IEnumerable<KeyValuePair<string, object>> map)
        {
            return "?" + string.Join("&", map.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
        public static string GetQueryString(this object obj)
        {
            return "?" + string.Join("&", obj.GetType().GetProperties(System.Reflection.BindingFlags.Public).Select(x => $"{x.Name}={x.GetValue(obj)}"));
        }
    }

}