using System.Drawing;
using TagsCloudContainer.Core.LayoutAlgorithms;

namespace TagsCloudContainer.Extensions
{
    static class PointExtensions
    {
        public static int SelectCoordinatePointAlongAxis(this Point point, Axis axis) =>
            axis == Axis.X ? point.X : point.Y;

        public static Point Add(this Point first, Point second) => new Point(first.X + second.X, first.Y + second.Y);
    }
}