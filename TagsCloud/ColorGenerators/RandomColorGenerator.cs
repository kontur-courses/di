using System.Drawing;
using TagsCloud.Entities;

namespace TagsCloud.ColorGenerators;

public class RandomColorGenerator: IColorGenerator
{
    private static readonly Random Random = new Random();

    public Color GetTagColor(Tag tag)
    {
        return Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256));
    }
}