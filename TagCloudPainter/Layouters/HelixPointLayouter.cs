using System.Drawing;

namespace TagCloudPainter.Layouters;

public class HelixPointLayouter
{
    private readonly double angleStep;
    private readonly Point center;
    private readonly double radiusStep;
    private double angle;
    private double radius;

    public HelixPointLayouter(Point center, double angleStep, double radiusStep)
    {
        if (radiusStep <= 0 || angleStep == 0)
            throw new ArgumentOutOfRangeException();
        this.center = center;
        this.angleStep = angleStep;
        this.radiusStep = radiusStep;
    }

    public Point GetPoint()
    {
        var x = center.X + GetX(radius, angle);
        var y = center.Y + GetY(radius, angle);
        var location = new Point(x, y);
        angle += angleStep;
        radius += radiusStep;
        return location;
    }

    private static int GetX(double r, double a) => (int)(r * Math.Cos(a));

    private static int GetY(double r, double a) => (int)(r * Math.Sin(a));
}