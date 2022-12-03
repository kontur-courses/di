using System.Drawing;

namespace TagsCloudContainer;

public static class RectangleExtensions
{
    public static bool TouchesWith(this Rectangle rectangle, Rectangle other)
    {
        return !rectangle.IntersectsWith(other) && (
            (rectangle.Left == other.Right && BoundsTopBottom(rectangle, other))
            || (rectangle.Right == other.Left && BoundsTopBottom(rectangle, other))
            || (rectangle.Top == other.Bottom && BoundsLeftRight(rectangle, other))
            || (rectangle.Bottom == other.Top && BoundsLeftRight(rectangle, other)));

        static bool BoundsTopBottom(Rectangle rectangle, Rectangle other)
        {
            return rectangle.Top <= other.Bottom && rectangle.Bottom >= other.Top;
        }

        static bool BoundsLeftRight(Rectangle rectangle, Rectangle other)
        {
            return rectangle.Left <= other.Right && rectangle.Right >= other.Left;
        }
    }

    public static Point Center(this Rectangle rectangle)
    {
        return new(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
    }

    public static Rectangle GetShiftedRectangle(this Rectangle rectangle, Point offset)
    {
        rectangle.Offset(offset);
        return rectangle;
    }

    public static Point GetFarthestDeltaFromTarget(this Rectangle rectangle, Point target)
    {
        return new(Math.Max(Math.Abs(target.X - rectangle.Left),
                Math.Abs(target.X - rectangle.Right)),
            Math.Max(Math.Abs(target.Y - rectangle.Top),
                Math.Abs(target.Y - rectangle.Bottom)));
    }
}