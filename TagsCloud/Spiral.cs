using System.Drawing;

namespace TagsCloud;

public class Spiral : ISpiral
{
    private int counter;
    private readonly float step;
    private readonly Point center;

    public Spiral(Point center, float step = 0.1f)
    {
        this.center = center;
        if (step == 0)
            throw new ArgumentException("the step must not be equal to 0");
        this.step = step;
    }

    public Point GetPoint()
    {
        var angle = step * counter;
        var xOffset = (float)(step * angle * Math.Cos(angle));
        var yOffset = (float)(step * angle * Math.Sin(angle));
        var point = new Point((int)Math.Round(center.X + xOffset), (int)Math.Round(center.Y + yOffset));
        counter += 1;
        return point;
    }
}