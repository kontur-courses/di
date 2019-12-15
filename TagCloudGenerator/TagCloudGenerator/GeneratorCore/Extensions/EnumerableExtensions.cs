using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloudGenerator.GeneratorCore.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SequenceShuffle<T>(this IEnumerable<T> sequence, Random random) =>
            sequence.OrderBy(item => random.Next());
    }
}