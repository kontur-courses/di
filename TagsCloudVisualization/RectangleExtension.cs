using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    static class RectangleExtension
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            return new Point(rectangle.Left + rectangle.Width/2, rectangle.Top + rectangle.Height / 2);
        }

        public static bool IntersectsWith(this Rectangle rectangles, IEnumerable<Rectangle> othRectangles)
        {
            return othRectangles.Any(x => x.IntersectsWith(rectangles));
        }
    }
}
