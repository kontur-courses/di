using System;
using System.Collections.Generic;
using System.Threading;

namespace TagsCloudVisualisation.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> UntilCanceled<T>(this IEnumerable<T> source, CancellationToken token)
        {
            foreach (var item in source)
            {
                yield return item;
                if (token.IsCancellationRequested)
                    break;
            }
        }

        public static IEnumerable<T> OnEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                yield return item;
                action.Invoke(item);
            }
        }
    }
}