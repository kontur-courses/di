using System.Drawing;

namespace TagsCloudVisualization;

public class SwampPointGenerator : IPointGenerator
{
    public Point GetNextPoint()
    {
        var random = new Random();
        var x = random.Next(-500, 500);
        var y = random.Next(-500, 500);
        return new Point(x, y);
    }
}