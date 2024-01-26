using System.Drawing;

namespace TagsCloudCore.Drawing.Colorers;

public class BicolorColorer : IWordColorer
{
    private readonly Color _first = Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
    private readonly Color _second = Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
    private static readonly Random Random = new();

    public Color GetWordColor(string word, int wordFrequency)
        => Random.Next(0, 2) == 0 ? _first : _second;
}