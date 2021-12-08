using System;
using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter.VectorsGenerator
{
    public class RandomVectorsGenerator : IVectorsGenerator
    {
        private readonly Random _random;
        private readonly Size _sizeRange;

        public RandomVectorsGenerator(Random random, Size sizeRange)
        {
            _random = random ?? throw new ArgumentNullException(nameof(random));
            if (sizeRange.Width <= 0 || sizeRange.Height <= 0)
                throw new ArgumentException(
                    $"Expected positive dimensions of size, but actual width = {sizeRange.Width}, height = {sizeRange.Height}");
            _sizeRange = sizeRange;
        }

        public Point GetNextVector() =>
            new(GenerateFromSegment(-_sizeRange.Width, _sizeRange.Width),
                GenerateFromSegment(-_sizeRange.Height, _sizeRange.Height));

        private int GenerateFromSegment(int min, int max) => _random.Next(max - min) + min;
    }
}