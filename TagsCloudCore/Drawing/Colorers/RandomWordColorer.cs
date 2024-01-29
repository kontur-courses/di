using System.Drawing;
using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.Drawing.Colorers;

public class RandomWordColorer : IWordColorer
{
    private static readonly Random Random = new();

    public Color GetWordColor(string word, int wordFrequency)
    {
        return Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
    }

    public WordColorerAlgorithm AlgorithmName => WordColorerAlgorithm.Random;
}