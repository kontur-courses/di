using System;

namespace TagsCloudVisualisation.Extensions
{
    internal static class Randomized
    {
        private static readonly Random randomSeed = new Random();

        public static T ItemOf<T>(T[] source) => source[randomSeed.Next(0, source.Length)];
    }
}