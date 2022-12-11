using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter.PointGenerator;

public class RandomPointGenerator : IPointGenerator
{
    private const int min = 50;
    private const int max = 200;
    private readonly Random random;

    public RandomPointGenerator(Random random)
    {
        this.random = random;
    }
    public Point Next()
    {
        return new Point(random.Next(min, max), random.Next(min, max));
    }
}