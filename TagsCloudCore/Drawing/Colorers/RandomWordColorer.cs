using System.Drawing;

namespace TagsCloudCore.Drawing.Colorers;

public class RandomWordColorer : IWordColorer
{
    private static readonly Random Random = new();
    
    public Color GetWordColor(string word, int wordFrequency)
        => Color.FromArgb(Random.Next(0, 256), Random.Next(0, 256), Random.Next(0, 256));
}