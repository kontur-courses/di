using System.Drawing;

namespace TagsCloudVisualization.Layouter
{
    public static class PointExtension
    {
        public static Point Add(this Point point1, Point point2) =>
            new Point(point1.X + point2.X, point1.Y + point2.Y);
    }
}
