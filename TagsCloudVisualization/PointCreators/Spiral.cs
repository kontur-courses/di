using System.Drawing;

namespace TagsCloudVisualization.PointCreators;

public class Spiral : IPointCreator
{
    private readonly Point center;
    private readonly double deltaAngle;
    private readonly double deltaRadius;

    public Spiral(Point center, double deltaAngle, double deltaRadius)
    {
        if (deltaRadius <= 0 || deltaAngle <= 0)
        {
            throw new ArgumentException("deltaRadius and deltaAngle must be positive");
        }

        this.center = center;
        this.deltaAngle = deltaAngle;
        this.deltaRadius = deltaRadius;
    }

    public IEnumerable<Point> GetPoints()
    {
        var angle = 0.0;
        var radius = 0.0;
        while (true)
        {
            var point = ConvertFromPolarCoordinates(angle, radius);
            point.Offset(center);
            yield return point;
            angle += deltaAngle;
            radius += deltaRadius;
        }
    }

    public static Point ConvertFromPolarCoordinates(double angle, double radius)
    {
        var x = (int)Math.Ceiling(Math.Cos(angle) * radius);
        var y = (int)Math.Ceiling(Math.Sin(angle) * radius);
        return new Point(x, y);
    }
}
