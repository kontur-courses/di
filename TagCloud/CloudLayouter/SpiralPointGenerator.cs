using System.Drawing;
using TagCloud.Abstractions;

namespace TagCloud;

public class SpiralPointGenerator : IPointGenerator
{
    private readonly double spiralExpansionStep;
    private readonly double spiralTwistStep;
    private readonly double xFlattening;

    public SpiralPointGenerator(double spiralExpansionStep, double spiralTwistStep, double xFlattening)
    {
        this.spiralExpansionStep = spiralExpansionStep;
        this.spiralTwistStep = spiralTwistStep;
        this.xFlattening = xFlattening;
    }

    public IEnumerable<Point> Generate(Point center)
    {
        double r = 0;
        double phi = 0;
        while (true)
        {
            r += spiralExpansionStep;
            phi += spiralTwistStep;
            var x = (int)(xFlattening * (r * Math.Cos(phi))) + center.X;
            var y = (int)(r * Math.Sin(phi)) + center.Y;
            yield return new Point(x, y);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}