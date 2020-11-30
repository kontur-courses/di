using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class IEnumerableExtensions
    {
        internal static IEnumerable<(T, int)> MostFrequent<T>(
            this IEnumerable<T> source, int count)
        {
            return source
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count())
                .OrderByDescending(pair => pair.Value)
                .Take(count)
                .Select(pair => (pair.Key, pair.Value));
        }
    }
}