using System.Drawing;

namespace TagCloud;

public class SpiralPointGenerator: IPointGenerator
{
    private readonly double spiralExpansionStep;
    private readonly double spiralTwistStep;
    
    public SpiralPointGenerator(double spiralExpansionStep, double spiralTwistStep)
    {
        this.spiralExpansionStep = spiralExpansionStep;
        this.spiralTwistStep = spiralTwistStep;
    }
    
    public IEnumerable<Point> Generate(Point center)
    {
        double r = 0;
        double phi = 0;
        while (true)
        {
            r += spiralExpansionStep;
            phi += spiralTwistStep;
            var x = (int)(r * Math.Cos(phi)) + center.X;
            var y = (int)(r * Math.Sin(phi)) + center.Y;
            yield return new Point(x, y);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}