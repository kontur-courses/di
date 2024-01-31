using System.Drawing;

namespace TagsCloud.Distributors;

public class SpiralDistributor : IDistributor
{
    public double Angle { get; private set; }
    public double Radius { get; private set; }
    public readonly double AngleStep;
    public readonly double RadiusStep;
    public readonly Point Center;

    public SpiralDistributor(Point center = new Point(), double angleStep = 0.1, double radiusStep = 0.1)
    {
        if (radiusStep <= 0 || angleStep == 0) throw new ArgumentException();
        this.Center = center;
        Radius = 0;
        Angle = 0;
        this.AngleStep = angleStep - 2 * Math.PI * (int)(angleStep / 2 * Math.PI);
        this.RadiusStep = radiusStep;
    }


    public Point GetNextPosition()
    {
        var x = Radius * Math.Cos(Angle) + Center.X;
        var y = Radius * Math.Sin(Angle) + Center.Y;

        Angle += AngleStep;

        if (Angle >= Math.PI * 2)
        {
            Angle -= 2 * Math.PI;
            Radius += RadiusStep;
        }

        return new Point((int)x, (int)y);
    }
}