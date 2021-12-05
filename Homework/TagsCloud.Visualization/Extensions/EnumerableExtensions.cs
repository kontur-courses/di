using System;
using System.Collections.Generic;

namespace TagsCloud.Visualization.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> func)
        {
            foreach (var item in collection)
                func(item);
        }
    }
}