using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Tuple<T, T>> CartesianSquare<T>(this IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();
            return list.SelectMany(x => list, Tuple.Create);
        }
    }
}