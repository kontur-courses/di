using System.Drawing;

namespace TagsCloudVisualization;

public static class Config
{
    public const int
        WindowWidth = 600,
        WindowHeight = 400,
        CenterX = 300,
        CenterY = 200;

    public const string
        DefaultPath = "..\\..\\..\\input.txt";

    public static Brush TextBrush => new SolidBrush(Color.Blue);
}