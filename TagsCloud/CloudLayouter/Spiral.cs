using System.Drawing;

namespace TagsCloud.CloudLayouter;

public class Spiral : ISpiral
{
    private readonly float step;
    private Point center;
    private int counter;

    public Spiral(Point center, float step = 0.1f)
    {
        Init(center);
        if (step == 0)
            throw new ArgumentException("the step must not be equal to 0");
        this.step = step;
    }

    public void Init(Point center)
    {
        counter = 0;
        this.center = center;
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