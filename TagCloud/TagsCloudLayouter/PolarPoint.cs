using System.Drawing;

namespace TagsCloudLayouter;

public readonly struct PolarPoint
{
    public readonly double Angle;
    public readonly double Radius;

    public PolarPoint(double radius, double angle)
    {
        Angle = angle;
        Radius = radius;
    }

    public static Point ToCartesian(PolarPoint polarPoint)
    {
        var x = (int)Math.Round(polarPoint.Radius * Math.Cos(polarPoint.Angle));
        var y = (int)Math.Round(polarPoint.Radius * Math.Sin(polarPoint.Angle));
        return new Point(x, y);
    }
}