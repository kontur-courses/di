using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> TakeSkip<T>(this IEnumerable<T> enumerable, int countToSkip, out T[] skipped)
        {
            var skippedEnumerable = enumerable.TakeSkip(countToSkip);
            skipped = skippedEnumerable.SkippedElements.ToArray();
            return skippedEnumerable;
        }

        public static SkippedEnumerable<T> TakeSkip<T>(this IEnumerable<T> enumerable, int toSkip)
        {
            var skipped = new T[toSkip];
            var enumerator = enumerable.GetEnumerator();
            for (var i = 0; i < toSkip; i++)
            {
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException("No elements remaining in source enumerable. " +
                                                        $"Total enumerable elements count: {i + 1}, " +
                                                        $"requested to skip: {toSkip}");
                skipped[i] = enumerator.Current;
            }

            return new SkippedEnumerable<T>(enumerator, skipped);
        }

        public class SkippedEnumerable<T> : IEnumerable<T>
        {
            public readonly IReadOnlyCollection<T> SkippedElements;
            private readonly IEnumerator<T> enumerator;

            public SkippedEnumerable(IEnumerator<T> enumerator, IReadOnlyCollection<T> skippedElements)
            {
                this.enumerator = enumerator;
                SkippedElements = skippedElements;
            }

            public IEnumerator<T> GetEnumerator() => enumerator;
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}