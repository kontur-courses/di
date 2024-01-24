using System.Drawing;

namespace TagCloud.PointGenerator;

public class CirclesGenerator : IPointGenerator
{
    private readonly Point startPoint;
    private int density;
    private readonly double angleShift;
    private double currentAngle;

    public string GeneratorName => "Circular";

    public CirclesGenerator(Point startPoint, int density = 1, double angleShift = 0.01)
    {
        if (startPoint.X < 0 || startPoint.Y < 0)
            throw new ArgumentException("Circle center point coordinates should be non-negative");
        this.startPoint = startPoint;
        this.density = density * 200;
        this.angleShift = angleShift;
    }

    public Point GetNextPoint()
    {
        var radius = density;
        var x = (int)(Math.Cos(currentAngle) * radius);
        var y = (int)(Math.Sin(currentAngle) * radius);
        currentAngle += angleShift;

        if (currentAngle < 2 * Math.PI && currentAngle + angleShift > 2 * Math.PI)
            density += 100;

        return new Point(startPoint.X + x, startPoint.Y + y);
    }
}