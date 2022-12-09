using System.Drawing;

namespace TagsCloud.Core.Helpers;

public static class RectangleCreator
{
	public static Rectangle GetRectangle(Point rectangleCenter, Size rectangleSize)
	{
		var x = rectangleCenter.X - rectangleSize.Width / 2;
		var y = rectangleCenter.Y - rectangleSize.Height / 2;
		return new Rectangle(new Point(x, y), rectangleSize);
	}
}