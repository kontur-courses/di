using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public static class EnumerableExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IEnumerable<(TKey key, TValue value)> keyValuePairs)
        {
            return keyValuePairs.ToDictionary(pair => pair.key, pair => pair.value);
        }
    }
}
