using System.Drawing;

namespace TagCloud
{
    public static class PointExtention
    {
        public static Point GetLeftTopCorner(this Point center, Size rectSize)
        {
            return new Point(center.X - rectSize.Width / 2, center.Y - rectSize.Height / 2);
        }
    }
}