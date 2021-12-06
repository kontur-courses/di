using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            var list = collection.ToList();
            var rand = new Random();

            for (var i = list.Count - 1; i >= 1; i--)
            {
                var j = rand.Next(i + 1);
                (list[j], list[i]) = (list[i], list[j]);
            }

            return list;
        }
    }
}