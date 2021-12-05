using System.Drawing;

namespace CloudLayouter;

public static class DrawingExtensions
{
    public static double DistanceTo(this Point from, Point to)
    {
        var dX = from.X - to.X;
        var dY = from.Y - to.Y;
        return Math.Sqrt(dX * dX + dY * dY);
    }

    public static double Length(this Point from)
    {
        return Math.Sqrt(from.X * from.X + from.Y * from.Y);
    }

    public static Point GetCenter(this Rectangle rectangle)
    {
        return rectangle.Location + rectangle.Size / 2;
    }

    public static IEnumerable<Point> GetAllPoints(this Rectangle rectangle)
    {
        yield return rectangle.Location;
        yield return new Point(rectangle.Right, rectangle.Top);
        yield return new Point(rectangle.Right, rectangle.Bottom);
        yield return new Point(rectangle.Left, rectangle.Bottom);
    }

    public static int GetArea(this Size size)
    {
        return size.Width * size.Height;
    }

    public static Size Abs(this Size size)
    {
        return new Size(Math.Abs(size.Width), Math.Abs(size.Height));
    }
}
