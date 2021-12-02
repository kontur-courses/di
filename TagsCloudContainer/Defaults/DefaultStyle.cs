﻿using System.Drawing;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class DefaultStyle : IStylerSettings
{
    public FontFamily FontFamily { get; set; } = FontFamily.GenericMonospace;

    public double MaxSize { get; set; } = 50;

    public Brush Brush { get; set; } = Brushes.White;

    public (Font font, Brush brush) GetStyle(ITag tag)
    {
        var font = new Font(FontFamily, (float)(tag.RelativeSize * MaxSize));
        return (font, Brush);
    }
}
