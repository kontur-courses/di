using System.Drawing;

namespace TagsCloud.ColorGenerators;

public class RandomColorGenerator: IColorGenerator
{
    private static readonly Random Random = new Random();

    public Color GetTagColor()
    {
        return Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
    }
}