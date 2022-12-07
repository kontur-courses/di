using System.Drawing;

namespace TagsCloudVisualization;

public class ArithmeticSpiral
{
    private readonly double _constant;
    public Point Center { get; }

    public ArithmeticSpiral(Point center, int constant = 1)
    {
        if (constant <= 0)
            throw new ArgumentException("Negative or zero arithmetic spiral constant not allowed");
       
        Center = center;
        _constant = constant;
    }

    public Point GetPoint(double length)
    {
        var newX = (int)(Center.X + Math.Cos(length) * length * _constant);
        var newY = (int)(Center.Y + Math.Sin(length) * length * _constant);

        return new Point(newX, newY);
    }
}