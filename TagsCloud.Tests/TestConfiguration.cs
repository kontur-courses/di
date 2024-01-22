using SixLabors.ImageSharp;

namespace TagsCloud.Tests;

public static class TestConfiguration
{
    public const float WindowWidth = 1920f;
    public const float WindowHeight = 1080f;

    public static Size ImageCenter => new((int)WindowWidth / 2, (int)WindowHeight / 2);
}