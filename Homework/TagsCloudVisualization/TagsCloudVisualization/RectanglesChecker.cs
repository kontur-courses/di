using System.Drawing;

namespace TagsCloudVisualization
{
    public static class RectanglesChecker
    {
        public static bool HaveIntersection(Rectangle first, Rectangle second)
        {
            return first.Left <= second.Right && first.Right >= second.Left && first.Top <= second.Bottom &&
                   first.Bottom >= second.Top &&
                   !IsNestedRectangle(first, second) && !IsNestedRectangle(second, first);
        }

        public static bool IsNestedRectangle(Rectangle first, Rectangle second)
        {
            return IsPointInRectangle(new Point(first.Left, first.Top), second) &&
                   IsPointInRectangle(new Point(first.Right, first.Bottom), second);
        }

        public static bool IsPointInRectangle(Point point, Rectangle rectangle)
        {
            return point.X > rectangle.Left && point.X < rectangle.Right &&
                   point.Y > rectangle.Top && point.Y < rectangle.Bottom;
        }
    }
}
