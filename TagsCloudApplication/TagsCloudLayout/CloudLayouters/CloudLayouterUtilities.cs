using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudLayout.PointLayouters;

namespace TagsCloudLayout.CloudLayouters
{
    public static class CloudLayouterUtilities
    {
        private readonly static Random rnd = new Random();

        public static List<Rectangle> LayoutRectangles(Point center, IEnumerable<Size> sizes)
        {
            var layouter = new CircularCloudLayouter(new ArchimedeanSpiral(center));
            return sizes.Select(size => layouter.PutNextRectangle(size)).ToList();
        }

        public static List<Rectangle> GenerateRandomLayout(Point center, int amount,
            int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            var sizes = new List<Size>();
            while (amount-- > 0)
                sizes.Add(GetRandomSize(minWidth, maxWidth, minHeight, maxHeight));
            return LayoutRectangles(center, sizes);
        }

        public static Size GetRandomSize(int minWidth, int maxWidth,
                                         int minHeight, int maxHeight)
        {
            return new Size(rnd.Next(minWidth, maxWidth + 1),
                                rnd.Next(minHeight, maxHeight + 1));
        }
    }
}
