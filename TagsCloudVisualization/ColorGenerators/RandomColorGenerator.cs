using System.Drawing;

namespace TagsCloudVisualization.ColorGenerators;

public class RandomColorGenerator : IColorGenerator
{
    private readonly Random random;

    public RandomColorGenerator()
    {
        random = new Random((int)DateTime.Now.Ticks);
    }

    public Color GetColor()
    {
        return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
    }
}