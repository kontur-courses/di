using System.Drawing;
using TagsCloudVisualization.Common;

namespace TagsCloudVisualization.PointsProviders;

public class ArchimedeanSpiralPointsProvider : IPointsProvider
{
    private readonly ArchimedeanSpiralSettings settings;
    public Point Start => settings.Center;

    public ArchimedeanSpiralPointsProvider(ArchimedeanSpiralSettings settings)
    {
        if (settings.DeltaAngle == 0 || settings.Distance == 0)
            throw new ArgumentException("deltaAngle and distance should not equals zero");
        this.settings = settings;
    }

    public IEnumerable<Point> GetPoints()
    {
        for (var angle = 0d; ; angle += settings.DeltaAngle)
        {
            var point = Start;
            point.Offset(PolarToCartesian(settings.Distance * angle, angle));

            yield return point;
        }
    }
    
    public static Point PolarToCartesian(double distance, double angle)
    {
        return new Point((int)(distance * Math.Cos(angle)), (int)(distance * Math.Sin(angle)));
    }
}