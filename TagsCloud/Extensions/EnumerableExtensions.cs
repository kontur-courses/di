using System.Collections.Generic;
using System.Linq;

namespace TagsCloud.Extensions
{
    public static class EnumerableExtensions
    {
        public static IReadOnlyDictionary<T, double> GetFrequency<T>(this IEnumerable<T> enumerable)
        {
            return enumerable
                .GroupBy(x => x)
                .ToDictionary(pair => pair.Key,
                    pair => pair.Count() / (double) enumerable.Count());
        }
    }
}
