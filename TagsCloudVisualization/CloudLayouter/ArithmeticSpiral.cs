using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter;

public class ArithmeticSpiral : ISpiralFormula
{
    private readonly float _constant;

    public ArithmeticSpiral(float constant = 1F)
    {
        if (constant <= 0)
            throw new ArgumentException("Negative or zero arithmetic spiral constant not allowed");

        _constant = constant;
    }

    public PointF GetPoint(PointF center, float length)
    {
        var newX = center.X + (float)Math.Cos(length) * length * _constant;
        var newY = center.Y + (float)Math.Sin(length) * length * _constant;

        return new PointF(newX, newY);
    }
}