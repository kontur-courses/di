using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> SortByFrequency<TSource>(
            this IEnumerable<TSource> enumerable)
        {
            var frequency = new Dictionary<TSource, int>();

            foreach (var item in enumerable)
            {
                if (!frequency.ContainsKey(item))
                    frequency[item] = 0;

                frequency[item]++;
            }

            return frequency
                .OrderByDescending(el => el.Value)
                .Select(el => el.Key);
        }
    }
}