using System.Drawing;

namespace TagCloud.Common.Options;

public class DrawingOptions
{
    public Size ImageSize { get; }
    public Color BackgroundColor { get; }
    public Color TextColor { get; }

    public DrawingOptions(Color backgroundColor, Size imageSize, Color textColor)
    {
        BackgroundColor = backgroundColor;
        ImageSize = imageSize;
        TextColor = textColor;
    }
}