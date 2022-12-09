using System.Drawing;

namespace TagsCloud.Core.Helpers;

public static class CoordinatesConverter
{
	public static Point ToCartesian(double polarRadiusInPixels, double polarAngleInRadian)
	{
		var x = polarRadiusInPixels * Math.Cos(polarAngleInRadian);
		var y = polarRadiusInPixels * Math.Sin(polarAngleInRadian);

		return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
	}

	public static (double polarRadiusInPixels, double polarAngleInRadian) ToPolar(Point cartesianCoordinates)
	{
		var polarAngle = Math.Atan2(cartesianCoordinates.Y, cartesianCoordinates.X);

		var polarRadius = Math.Sqrt(Math.Pow(cartesianCoordinates.X, 2) + Math.Pow(cartesianCoordinates.Y, 2));

		return (polarRadius, polarAngle);
	}
}