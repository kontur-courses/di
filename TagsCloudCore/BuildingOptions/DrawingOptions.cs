﻿using System.Drawing;

namespace TagsCloudCore.BuildingOptions;

public class DrawingOptions
{
    public DrawingOptions(Color fontColor, Color backgroundColor, Size imageSize, Font font, int frequencyScaling)
    {
        FontColor = fontColor;
        BackgroundColor = backgroundColor;
        ImageSize = imageSize;
        Font = font;
        FrequencyScaling = frequencyScaling;
    }

    public Color FontColor { get; }
    public Color BackgroundColor { get; }
    public Size ImageSize { get; }
    public Font Font { get; }
    public int FrequencyScaling { get; }
}