using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloudContainerTaskTests
{
    public static class RectangleSizeGenerator
    {
        private const int MinSize = 15;
        private const int MaxSize = 150;
        private const int DefaultWidth = 100;
        private const int DefaultHeight = 50;

        private static readonly Random Rnd;

        static RectangleSizeGenerator()
        {
            Rnd = new Random();
        }

        public static IEnumerable<Size> GetNextNFixedSizes(int n)
        {
            for (var i = 0; i < n; i++)
                yield return new Size(DefaultWidth, DefaultHeight);
        }

        public static IEnumerable<Size> GetNextNRandomSizes(int n)
        {
            for (var i = 0; i < n; i++)
            {
                var width = Rnd.Next(MinSize, MaxSize);
                var height = Rnd.Next(MinSize, MaxSize);
                yield return new Size(width, height);
            }
        }
    }
}