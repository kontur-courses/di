using System.Drawing;

namespace TagsCloud.Algorithms;

public class FlowerSpiral : ISpiral
{
    private int counter;
    private readonly float step;
    private readonly Point center;
    private readonly int petalCount;
    private readonly double petalLength;

    public FlowerSpiral(Point center, double petalLength = 0.25, int petalCount = 1, float step = 0.1f)
    {
        this.center = center;
        this.petalLength = petalLength;
        this.petalCount = petalCount;
        if (this.petalCount < 0 || petalLength < 0)
            throw new ArgumentException($"{nameof(petalCount)} or {nameof(petalLength)} must not be less than 0");
        if (step == 0)
            throw new ArgumentException($"the {nameof(step)} must not be equal to 0");
        this.step = step;
    }

    public Point GetPoint()
    {
        var angle = step * counter;
        var radius = angle * petalLength * Math.Sin(petalCount * angle);
        var xOffset = (float)(radius * Math.Cos(angle));
        var yOffset = (float)(radius * Math.Sin(angle));
        var point = new Point((int)Math.Round(center.X + xOffset), (int)Math.Round(center.Y + yOffset));
        counter += 1;
        return point;
    }
}