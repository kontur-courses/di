﻿using System;

namespace TagCloud.Core.Extensions
{
    internal static class Randomized
    {
        private static readonly Random randomSeed = new Random();

        public static T ItemFrom<T>(T[] source) => source[randomSeed.Next(0, source.Length)];
    }
}