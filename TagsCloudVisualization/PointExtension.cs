using System.Drawing;

namespace TagsCloudVisualization
{
    public static class PointExtension
    {
        public static Point ShiftToLeftRectangleCorner(this Point point, Size size)
        {
            return new Point(point.X - size.Width / 2, point.Y - size.Height / 2);
        }
    }
}
