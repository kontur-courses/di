using System.Drawing;

namespace TagCloud;

public class SpiralCloudShaper : ICloudShaper
{
    private Point center;
    private double coefficient;
    private double deltaAngle;
    
    private SpiralCloudShaper(Point center, double coefficient, double deltaAngle)
    {
        this.center = center;
        this.deltaAngle = deltaAngle;
        this.coefficient = coefficient;
    }
    
    public IEnumerable<Point> GetPossiblePoints()
    {
        var currentAngle = 0D;
        var position = center;
        var previousPoint = position;
        while(true)
        {
            while (position == previousPoint)
            {
                currentAngle += deltaAngle;
                previousPoint = position;
                position = CalculatePointByCurrentAngle(currentAngle);
            }
            yield return position;
            previousPoint = position;
            position = CalculatePointByCurrentAngle(currentAngle);
        }
    }

    private Point CalculatePointByCurrentAngle(double angle)
    {
        return new Point(
            center.X + (int)(coefficient * angle * Math.Cos(angle)), 
            center.Y + (int)(coefficient * angle * Math.Sin(angle))
        );
    }

    public static SpiralCloudShaper Create(Point center, double coefficient = 0.1, double deltaAngle = 0.1)
    {
        if (coefficient <= 0)
            throw new ArgumentException("Spiral coefficient must be positive number");
        if (deltaAngle <= 0)
            throw new ArgumentException("Spiral delta angle must be positive number");
        return new SpiralCloudShaper(center, coefficient, deltaAngle);
    }
}