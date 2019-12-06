using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TagCloudGenerator.Tests.Extensions
{
    public static class RandomExtensions
    {
        [SuppressMessage("ReSharper", "IteratorNeverReturns")]
        public static IEnumerable<int> GetRandomSequence(this Random random)
        {
            while (true)
                yield return random.Next();
        }
    }
}