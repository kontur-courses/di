using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainerTests.CloudLayouterTests
{
    public class SizeGenerator
    {
        private readonly Random random;
        private readonly int maxHeight;
        private readonly int maxWidth;
        private readonly int minHeight;
        private readonly int minWidth;

        public SizeGenerator(int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            random = new Random();
            this.minWidth = minWidth;
            this.maxWidth = maxWidth;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
        }

        public IEnumerable<Size> GenerateSizes(int count)
        {
            for (var i = 0; i < count; i++)
                yield return new Size(random.Next(minWidth, maxWidth), random.Next(minHeight, maxHeight));
        }
    }
}