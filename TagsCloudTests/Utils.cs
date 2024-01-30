using System.Drawing;

namespace TagsCloudTests;

public static class Utils
{
    private static readonly Random Random = new();
    private const int MinSize = 1;
    private const int MaxSize = 50;

    public static Size GetRandomSize() =>
        new Size(Random.Next(MinSize, MaxSize), Random.Next(MinSize, MaxSize));
}