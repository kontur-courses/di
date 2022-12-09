using System.Drawing;

namespace TagsCloud.Core.Helpers;

public static class PointExtensions
{
	public static Point Plus(this Point point, Point otherPoint)
	{
		return new Point(point.X + otherPoint.X, point.Y + otherPoint.Y);
	}

	public static Point Minus(this Point point, Point otherPoint)
	{
		return new Point(point.X - otherPoint.X, point.Y - otherPoint.Y);
	}
}