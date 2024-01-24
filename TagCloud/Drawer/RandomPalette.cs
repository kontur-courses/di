using System.Drawing;

namespace TagCloud.Drawer;

public class RandomPalette : IPalette
{
    private Random random = new();

    public Color ForegroundColor => Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    public Color BackgroudColor => Color.White;
}