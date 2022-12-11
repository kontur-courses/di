using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TagCloud.Extensions
{
    public static class EnumerableExtensions
    {
        public static TPropType MinOrDefault<T, TPropType>(
            this IEnumerable<T> sequence, 
            Expression<Func<T, TPropType>> memberSelector) => 
                sequence.IsEmpty() 
                    ? default 
                    : sequence.Min(memberSelector.Compile());

        public static TPropType MaxOrDefault<T, TPropType>(
            this IEnumerable<T> sequence, 
            Expression<Func<T, TPropType>> memberSelector) => 
                sequence.IsEmpty()
                    ? default
                    : sequence.Max(memberSelector.Compile());

        private static bool IsEmpty<T>(this IEnumerable<T> sequence) =>
            ! sequence.Any();
    }
}
