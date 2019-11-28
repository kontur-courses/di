using System;
using System.Drawing;

namespace TagsCloudVisualization
{
	public static class ExMath
	{
		public static Point ToCartesianCoordinateSystem(double radius, double angle, Point center)
		{
			var x = radius * Math.Cos(angle) + center.X;
			var y = radius * Math.Sin(angle) + center.Y;
			return new Point(RoundCoordinate(x, center.X), RoundCoordinate(y, center.Y));
		}		
		
		public static int RoundCoordinate(double value, int centerCoordinate)
		{
			if (value > 0)
				return value > centerCoordinate ? (int) Math.Ceiling(value) : (int) value;
			return value > centerCoordinate ? (int) value: (int) Math.Floor(value);
		}
	}
}