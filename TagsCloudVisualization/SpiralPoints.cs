using System.Drawing;

namespace TagsCloudVisualization;

public sealed class SpiralPoints : IPoints
{
    private readonly Point center;
    private readonly double radiusStep;

    public SpiralPoints(Point center, double radiusStep = 1)
    {
        this.center = center;
        this.radiusStep = radiusStep;
    }

    ///<exception cref="System.OverflowExeption">
    /// Long point generation operation
    /// </exception>
    public IEnumerable<Point> GetPoints()
    {
        var radius = 0d;
        var shift = radiusStep / 360;
        while (true)
        {
            for (var pointNumber = 0; pointNumber < 360; pointNumber++)
            {
                var pointAngle = 2 * Math.PI * pointNumber / 360;
                var currentPoint = new Point(center.X, center.Y);
                currentPoint.Offset((int)(Math.Cos(pointAngle) * radius), (int)(Math.Sin(pointAngle) * radius));
                yield return currentPoint;
                radius += shift;
            }
        }
    }
}