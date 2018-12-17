using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.CircularCloudLayouter
{
    public static class EnumerableRectanglesExtension
    {
        public static Size GetImageSize(this IEnumerable<Rectangle> rectangles)
        {
            var minX = 0;
            var maxX = 0;
            var minY = 0;
            var maxY = 0;

            foreach (var rectangle in rectangles)
            {
                if (rectangle.X <= minX)
                    minX = rectangle.X;
                if (rectangle.X + rectangle.Width >= maxX)
                    maxX = rectangle.X + rectangle.Width;
                if (rectangle.Y - rectangle.Height <= minY)
                    minY = rectangle.Y - rectangle.Height;
                if (rectangle.Y >= maxY)
                    maxY = rectangle.Y;
            }

            var width = Math.Abs(minX) + Math.Abs(maxX) + 1;
            var height = Math.Abs(minY) + Math.Abs(maxY) + 1;

            return new Size(width, height);
        }
        
        public static int GetMinX(this IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Select(r => r.X).Min();
        }

        public static int GetMaxX(this IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Select(r => r.X).Max();
        }

        public static int GetMinY(this IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Select(r => r.Y).Min();
        }

        public static int GetMaxY(this IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Select(r => r.Y).Max();
        }

        public static int GetYHeight(this IEnumerable<Rectangle> rectangles)
        {
            return rectangles.Select(r => r.Y - r.Height).Min();
        }
    }
}