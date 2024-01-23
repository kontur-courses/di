using System.Drawing;

namespace TagsCloudVisualization;

public class Palette
{
    public Palette(Color textColor, Color backgroundColor)
    {
        TextColor = textColor;
        BackgroundColor = backgroundColor;
    }

    public Color TextColor { get; set; }
    public Color BackgroundColor { get; set; }
}