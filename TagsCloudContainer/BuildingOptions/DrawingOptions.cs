using System.Drawing;

namespace TagsCloudContainer.BuildingOptions;

public class DrawingOptions
{
    public Color FontColor { get; }
    public Color BackgroundColor { get; }
    public Size ImageSize { get; }
    public Font Font { get; }
    public int FrequencyScaling { get; }

    public DrawingOptions(Color fontColor, Color backgroundColor, Size imageSize, Font font, int frequencyScaling)
    {
        FontColor = fontColor;
        BackgroundColor = backgroundColor;
        ImageSize = imageSize;
        Font = font;
        FrequencyScaling = frequencyScaling;
    }
}