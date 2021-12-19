using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudVisualisation
{
    public static class RectangleExtensions
    {
        public static bool IntersectsWith(this Rectangle rectangle, IEnumerable<Rectangle> others)
        {
            return others.Select(x => x.IntersectsWith(rectangle)).Any(x => x);
        }

        public static double GetDiagonalLength(this Rectangle rect1)
        {
            var leftBottomCorner1 = new Point(rect1.Left, rect1.Bottom);
            var rightUpperCorner1 = new Point(rect1.Right, rect1.Top);
            return leftBottomCorner1.GetDistanceTo(rightUpperCorner1);
        }
    }
}
