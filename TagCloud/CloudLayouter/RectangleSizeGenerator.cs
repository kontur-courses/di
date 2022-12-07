using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public static class RectangleSizeGenerator
    {
        private static readonly Random Random = new Random();

        public static IReadOnlyList<Size> GetRandomSizesList(int count, Size minSize, Size maxSize)
        {
            var result = new List<Size>();

            for (var i = 0; i < count; i++)
            {
                var scaleFactor = Random.NextDouble();

                var width = Math.Max(minSize.Width, (int)(scaleFactor * maxSize.Width));

                var height = Math.Max(minSize.Height, (int)(scaleFactor * maxSize.Height));

                result.Add(new Size(width, height));
            }

            return result;
        }

        public static IReadOnlyList<Size> GetRandomOrderedSizes(int count, Size minSize, Size maxSize)
        {
            return GetRandomSizesList(count, minSize, maxSize)
                .OrderByDescending(s => s.Width * s.Height)
                .ToList();
        }

        public static IReadOnlyList<Size> GetConstantSizes(int count, Size size)
        {
            var result = new List<Size>();

            for (var i = 0; i < count; i++)
                result.Add(size);

            return result;
        }

    }
}