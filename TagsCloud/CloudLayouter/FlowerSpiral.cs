using System.Drawing;

namespace TagsCloud.CloudLayouter;

public class FlowerSpiral : ISpiral
{
    private readonly int petalCount;
    private readonly double petalLength;
    private readonly float step;
    private int counter;
    public FlowerSpiral(double petalLength = 0.5, int petalCount = 4, float step = 0.1f)
    {
        this.petalLength = petalLength;
        this.petalCount = petalCount;
        if (this.petalCount < 0 || petalLength < 0)
            throw new ArgumentException($"{nameof(petalCount)} or {nameof(petalLength)} must not be less than 0");
        if (step == 0)
            throw new ArgumentException($"the {nameof(step)} must not be equal to 0");
        this.step = step;
    }

    public IEnumerable<Point> GetPoints(Point center)
    {
        counter = 0;
        while (true)
        {
            var angle = step * counter;
            var radius = angle * petalLength * Math.Sin(petalCount * angle);
            var xOffset = (float)(radius * Math.Cos(angle));
            var yOffset = (float)(radius * Math.Sin(angle));
            yield return new Point((int)Math.Round(center.X + xOffset), (int)Math.Round(center.Y + yOffset));
            counter += 1;
        }
    }
}