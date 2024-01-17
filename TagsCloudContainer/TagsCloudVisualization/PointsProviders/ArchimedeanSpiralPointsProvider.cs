using System.Drawing;

namespace TagsCloudVisualization.PointsProviders;

public class ArchimedeanSpiralPointsProvider : IPointsProvider
{
    private readonly double deltaAngle;
    private readonly double distance;
    public Point Start { get; }

    public ArchimedeanSpiralPointsProvider(Point center, double deltaAngle = 5 * Math.PI / 180, double distance = 2)
    {
        if (deltaAngle == 0 || distance == 0)
            throw new ArgumentException("deltaAngle and distance should not equals zero");
        this.deltaAngle = deltaAngle;
        this.distance = distance;
        Start = center;
    }

    public IEnumerable<Point> GetPoints()
    {
        for (var angle = 0d; ; angle += deltaAngle)
        {
            var point = Start;
            point.Offset(PolarToCartesian(distance * angle, angle));

            yield return point;
        }
    }
    
    public static Point PolarToCartesian(double distance, double angle)
    {
        return new Point((int)(distance * Math.Cos(angle)), (int)(distance * Math.Sin(angle)));
    }
}