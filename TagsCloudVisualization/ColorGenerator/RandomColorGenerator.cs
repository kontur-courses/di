using System.Drawing;

namespace TagsCloudVisualization.ColorGenerator;

public class RandomColorGenerator : IColorGenerator
{
    private readonly Random random;

    public RandomColorGenerator(Random random)
    {
        this.random = random;
    }

    public Color Generate()
    {
        return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256), random.Next(256));
    }
}