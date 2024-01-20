using System.Drawing;

namespace TagsCloudContainer.DrawingOptions;

public class Options
{
    public Color FontColor { get; }
    public Color BackgroundColor { get; }
    public Size ImageSize { get; }
    public Font Font { get; }
    public int FrequencyScaling { get; }

    public Options(Color fontColor, Color backgroundColor, Size imageSize, Font font, int frequencyScaling)
    {
        FontColor = fontColor;
        BackgroundColor = backgroundColor;
        ImageSize = imageSize;
        Font = font;
        FrequencyScaling = frequencyScaling;
    }
}