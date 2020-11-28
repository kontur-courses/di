using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudUI.Extensions
{
    public static class IEnumerableExtensions
    {
        internal static IEnumerable<T> MostFrequent<T>(
            this IEnumerable<T> source, int count)
        {
            var dictionary = new Dictionary<T, int>();
            foreach (var element in source)
            {
                if (!dictionary.ContainsKey(element))
                    dictionary[element] = 0;
                dictionary[element]++;
            }

            return dictionary.OrderByDescending(pair => pair.Value)
                .Take(Math.Min(count, dictionary.Count))
                .Select(pair => pair.Key);
        }
    }
}