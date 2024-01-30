using System.Drawing;

namespace TagsCloud.CloudLayouter;

public class Spiral : ISpiral
{
    private readonly float step;

    public Spiral(float step = 0.1f)
    {
        if (step == 0)
            throw new ArgumentException("the step must not be equal to 0");
        this.step = step;
    }

    public IEnumerable<Point> GetPoints(Point start)
    {
        var counter = 0;
        while (true)
        {
            var angle = step * counter;
            var xOffset = (float)(step * angle * Math.Cos(angle));
            var yOffset = (float)(step * angle * Math.Sin(angle));
            var point = new Point((int)Math.Round(start.X + xOffset), (int)Math.Round(start.Y + yOffset));
            yield return point;
            counter += 1;
        }
    }
}