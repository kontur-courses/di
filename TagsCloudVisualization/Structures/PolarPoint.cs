using System.Drawing;

namespace TagsCloudVisualization;

public class PolarPoint
{
    private readonly double _angle;
    private readonly double _radius;

    public PolarPoint(double radius, double angle)
    {
        _radius = radius;
        _angle = angle;
    }

    public static explicit operator Point(PolarPoint polarPoint)
    {
        var X = (int)Math.Round(polarPoint._radius * Math.Cos(polarPoint._angle));
        var Y = (int)Math.Round(polarPoint._radius * Math.Sin(polarPoint._angle));
        return new Point(X, Y);
    }
}