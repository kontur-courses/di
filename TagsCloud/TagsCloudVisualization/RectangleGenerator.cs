using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleGenerator
    {
        public static List<RectangleF> GenerateRandomRectangles(SpiralCloudLayouter layouter, int count, int minSize,
            int maxSize, Random random)
        {
            return Enumerable.Range(0, count)
                .Select(x => layouter
                .PutNextRectangle(new SizeF(random.Next(minSize, maxSize), random.Next(minSize, maxSize))))
                .ToList();
        }
    }
}