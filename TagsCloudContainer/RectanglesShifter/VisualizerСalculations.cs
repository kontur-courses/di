using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.RectanglesShifter
{
    static class VisualizerСalculations
    {
        public static Size GetOptimalSizeForImage(IList<Rectangle> rectangles, int indent)
        {
            if (rectangles.Count == 0)
                throw new ArgumentException("Empty rectangles list");
            var minTop = rectangles.Min(rect => rect.Top);
            var maxBottom = rectangles.Max(rect => rect.Bottom);
            var maxRight = rectangles.Max(rect => rect.Right);
            var minLeft = rectangles.Min(rect => rect.Left);
            var width = maxRight - minLeft;
            var height = maxBottom - minTop;
            return new Size(width + indent * 2, height + indent * 2);
        }

        public static Point GetCenter(Size size)
        {
            return new Point(size.Width / 2, size.Height / 2);
        }

        public static List<Rectangle> GetRectanglesWithOptimalLocation(IList<Rectangle> rectangles, Size offset)
        {
            return rectangles
                .Select(rectangle => new Rectangle(rectangle.Location + offset, rectangle.Size))
                .ToList();
        }
    }
}
