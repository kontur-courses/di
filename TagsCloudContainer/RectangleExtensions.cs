using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            var startPoint = rectangle.Location;
            return new Point(startPoint.X + rectangle.Width / 2, startPoint.Y + rectangle.Height / 2);
        }
        
        public static bool IsIntersects(this Rectangle newRectangle, List<Rectangle> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                if (rectangle.IntersectsWith(newRectangle))
                    return true;
            }

            return false;
        }
    }
}