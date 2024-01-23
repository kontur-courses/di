using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.PointCreators;

public class Spiral : IPointCreator
{
    private readonly Point center;
    private readonly double deltaAngle;
    private readonly double deltaRadius;

    public Spiral(ImageSettings imageSettings, SpiralSettings spiralSettings)
    {
        deltaRadius = spiralSettings.DeltaRadius;
        deltaAngle = spiralSettings.DeltaAngle;
        center = new Point(imageSettings.Width / 2, imageSettings.Height / 2);
        if (deltaRadius <= 0 || deltaAngle <= 0)
        {
            throw new ArgumentException("deltaRadius and deltaAngle must be positive");
        }
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
