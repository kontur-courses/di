using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Tests
{
    internal static class SizeGenerator
    {
        private static readonly Random Rnd = new Random();

        public static IEnumerable<Size> GenerateSimilarSquaresSize()
        {
            while (true)
            {
                var x = Rnd.Next(100, 200);
                yield return new Size(x, x);
            }
        }

        public static IEnumerable<Size> GenerateRandomSize()
        {
            while (true)
            {
                var x = Rnd.Next(1, 500);
                var y = Rnd.Next(1, 500);
                yield return new Size(x, y);
            }
        }

        public static IEnumerable<Size> GenerateRandomSquarersSize()
        {
            while (true)
            {
                var x = Rnd.Next(30, 600);
                yield return new Size(x, x);
            }
        }

        public static IEnumerable<Size> GenerateVerticalRectanglesSize()
        {
            while (true)
            {
                var x = Rnd.Next(30, 100);
                var y = Rnd.Next(100, 600);
                yield return new Size(x, y);
            }
        }

        public static IEnumerable<Size> GenerateHorizontalRectanglesSize()
        {
            while (true)
            {
                var x = Rnd.Next(100, 600);
                var y = Rnd.Next(30, 100);
                yield return new Size(x, y);
            }
        }

        public static IEnumerable<Size> GenerateRandomDecreasingRectanglesSize()
        {
            var counter = 1.0;
            while (true)
            {
                var x = Rnd.Next(300, 500);
                var y = Rnd.Next(300, 500);
                yield return new Size((int) (x * counter), (int) (y * counter));
                counter -= 0.005;
            }
        }

        public static IEnumerable<Size> GenerateRandomIncreasingRectanglesSize()
        {
            var counter = 1.0;
            while (true)
            {
                var x = Rnd.Next(10, 30);
                var y = Rnd.Next(10, 30);
                yield return new Size((int) (x * counter), (int) (y * counter));
                counter += 0.05;
            }
        }
    }
}