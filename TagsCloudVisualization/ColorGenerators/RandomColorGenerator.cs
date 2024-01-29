using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.ColorGenerators;

public class RandomColorGenerator : IColorGenerator
{
    private readonly Random random;
    private readonly bool isMatch;

    public RandomColorGenerator(ColorGeneratorSettings settings)
    {
        random = new Random((int)DateTime.Now.Ticks);
        isMatch = settings.Color == "random";
    }

    public Color GetColor()
    {
        return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
    }

    public bool Match()
    {
        return isMatch;
    }
}