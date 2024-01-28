using System.Drawing;

namespace TagsCloudVisualization;

public class Palette : IPalette
{
    public Palette(Color[] textColor, Color backgroundColor)
    {
        TextColor = textColor;
        BackgroundColor = backgroundColor;
    }

    private Color[] TextColor { get; set; }
    private int currentColorId = 0;
    private Color BackgroundColor { get; set; }

    public Color GetNextWordColor()
    {
        if (currentColorId >= TextColor.Length) currentColorId = 0;
        return TextColor[currentColorId++];
    }

    public Color GetBackgroundColor()
    {
        return BackgroundColor;
    }
}