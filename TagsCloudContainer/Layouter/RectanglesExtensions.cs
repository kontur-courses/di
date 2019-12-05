using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer.Layouter
{
    public static class RectanglesExtensions
    {
        public static List<Point> GetCorners(this Rectangle rectangle)
        {
            return new List<Point>
            {
                rectangle.Location,
                new Point(rectangle.Right, rectangle.Top),
                new Point(rectangle.Right, rectangle.Bottom),
                new Point(rectangle.Left, rectangle.Bottom)
            };
        }
    }
}
