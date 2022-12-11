using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter.PointGenerator;

public class SpiralPointGenerator : IPointGenerator
{
    private readonly Point center;
    private readonly double angleStep;
    private readonly int distance;
    private double angle;

    /// <summary>
    /// Generates points on the spiral
    /// </summary>
    /// <param name="center">Initial point</param>
    /// <param name="distance">Distance between turns of the spiral</param>
    /// <param name="angleStep">Step of the angle</param>
    /// <exception cref="ArgumentException"></exception>
    public SpiralPointGenerator(Point center, int distance = 1, double angleStep = 0.02)
    {
        if (distance == 0)
            throw new ArgumentException("Parameter should be not equal zero");
        if (angleStep == 0.0)
            throw new ArgumentException("Step should be not equal zero");
        this.center = center;
        this.angleStep = angleStep;
        this.distance = distance;
    }

    public Point Next()
    {
        if (angle == 0.0)
        {
            angle += angleStep;
            return center;
        }

        var x = Convert.ToInt32(distance * angle * Math.Cos(angle) + center.X);
        var y = Convert.ToInt32(distance * angle * Math.Sin(angle) + center.Y);
        angle += angleStep;

        return new Point(x, y);
    }
}