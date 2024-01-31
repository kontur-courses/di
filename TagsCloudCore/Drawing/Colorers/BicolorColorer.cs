using System.Drawing;
using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.Drawing.Colorers;

public class BicolorColorer : IWordColorer
{
    private static readonly Random Random = new();
    private readonly Color _first = Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
    private readonly Color _second = Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));

    public Color GetWordColor(string word, int wordFrequency)
    {
        return Random.Next(0, 2) == 0 ? _first : _second;
    }

    public WordColorerAlgorithm AlgorithmName => WordColorerAlgorithm.Bicolor;
}