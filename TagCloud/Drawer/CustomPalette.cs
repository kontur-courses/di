using System.Drawing;

namespace TagCloud.Drawer;

public class CustomPalette : IPalette
{
    private Color foregroundColor;
    private Color backgroundColor;

    public CustomPalette(Color foregroundColor, Color backgroundColor)
    {
        this.foregroundColor = foregroundColor;
        this.backgroundColor = backgroundColor;
    }

    public Color ForegroundColor => foregroundColor;
    public Color BackgroudColor => backgroundColor;
}