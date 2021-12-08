using System.Drawing;

namespace TagCloud.Infrastructure.Painter;

public class RandomPalette : IPalette
{
    private readonly Random random = new();

    public RandomPalette()
    {
        BackgroundColor = Color.Black;
    }

    public RandomPalette(Color backgroundColor)
    {
        BackgroundColor = backgroundColor;
    }

    public Color MainColor => Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    public Color BackgroundColor { get; }
}