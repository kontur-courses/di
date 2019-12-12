using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T, int)> MostCommon<T>(this IEnumerable<T> source, int count)
        {
            var dictionary = new Dictionary<T, int>();
            foreach (var element in source)
            {
                if (!dictionary.ContainsKey(element))
                    dictionary[element] = 0;
                dictionary[element]++;
            }

            return dictionary.OrderByDescending(kvp => kvp.Value)
                .Take(Math.Min(count, dictionary.Count))
                .Select(kvp => (kvp.Key, kvp.Value));
        }
    }
}