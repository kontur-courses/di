using System;
using System.Collections.Generic;
using System.Drawing;

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
            var minX = 0;
            foreach (var rectangle in rectangles)
                if (rectangle.X <= minX)
                    minX = rectangle.X;

            return minX;
        }

        public static int GetMaxX(this IEnumerable<Rectangle> rectangles)
        {
            var maxX = 0;
            foreach (var rectangle in rectangles)
                if (rectangle.X + rectangle.Width >= maxX)
                    maxX = rectangle.X + rectangle.Width;

            return maxX;
        }

        public static int GetMinY(this IEnumerable<Rectangle> rectangles)
        {
            var minY = 0;
            foreach (var rectangle in rectangles)
                if (rectangle.Y - rectangle.Height <= minY)
                    minY = rectangle.Y - rectangle.Height;

            return minY;
        }

        public static int GetMaxY(this IEnumerable<Rectangle> rectangles)
        {
            var maxY = 0;
            foreach (var rectangle in rectangles)
                if (rectangle.Y >= maxY)
                    maxY = rectangle.Y;

            return maxY;
        }

        public static int GetYHeight(this IEnumerable<Rectangle> rectangles)
        {
            var yHeight = 0;
            foreach (var rectangle in rectangles)
                if (rectangle.Y - rectangle.Height <= yHeight)
                    yHeight = rectangle.Y - rectangle.Height;

            return yHeight;
        }
    }
}