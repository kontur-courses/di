using System;
using System.Drawing;

namespace TagCloud.CloudLayouter
{
    public static class PointExtensions
    {
        public static Point MoveOn(this Point point, int deltaX, int deltaY)
        {
            return new Point(point.X  + deltaX, point.Y + deltaY);
        }
    }
}
