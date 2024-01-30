using System.Drawing;

namespace TagsCloudVisualization;

public class SpiralPointGenerator : IPointGenerator
{
    public Point Center { get; } = new(0, 0);
    public int Radius { get; private set; }
    public double Angle { get; private set; }
    public int RadiusDelta { get; private set; } = 1;
    public double AngleDelta { get; private set; } = Math.PI / 60;

    public Algorithm Name { get; } = Algorithm.Spiral;

    public Point GetNextPoint()
    {
        var x = (int)Math.Round(Center.X + Radius * Math.Cos(Angle));
        var y = (int)Math.Round(Center.Y + Radius * Math.Sin(Angle));

        var nextAngle = Angle + AngleDelta;
        var angleMoreThan2Pi = Math.Abs(nextAngle) >= Math.PI * 2;

        Radius = angleMoreThan2Pi ? Radius + RadiusDelta : Radius;
        Angle = angleMoreThan2Pi ? 0 : nextAngle;

        return new Point(x, y);
    }
}