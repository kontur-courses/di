using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Core
{
    public static class DictionaryExtension
    {
        public static void Add<T>(this Dictionary<T, int> source, T key)
        {
            if (!source.ContainsKey(key))
                source[key] = 0;
            source[key]++;
        }

        public static int GetCounter<T>(this Dictionary<T, int> source, T key) =>
            source.TryGetValue(key, out var result) ? result : 0;

        public static IEnumerable<(T, int)> Top<T>(this Dictionary<T, int> source, int count)
        {
            return source
                .OrderByDescending(kvp => kvp.Value)
                .Take(Math.Min(count, source.Count))
                .Select(kvp => (kvp.Key, kvp.Value));
        }
    }
}