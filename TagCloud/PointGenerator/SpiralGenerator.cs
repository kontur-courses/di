using System.Drawing;

namespace TagCloud.PointGenerator;

public class SpiralGenerator : IPointGenerator
{
    private readonly Point startPoint;
    private readonly int spiralDensity;
    private readonly double angleShift;
    private double currentAngle;

    public string GeneratorName => "Spiral";

    public SpiralGenerator(Point startPoint, int spiralDensity = 1, double angleShift = 0.01)
    {
        if (startPoint.X < 0 || startPoint.Y < 0)
            throw new ArgumentException("Spiral center point coordinates should be non-negative");
        this.startPoint = startPoint;
        this.spiralDensity = spiralDensity;
        this.angleShift = angleShift;
    }

    public Point GetNextPoint()
    {
        var radius = spiralDensity * currentAngle;
        var x = (int)(Math.Cos(currentAngle) * radius);
        var y = (int)(Math.Sin(currentAngle) * radius);
        currentAngle += angleShift;

        return new Point(startPoint.X + x, startPoint.Y + y);
    }
}