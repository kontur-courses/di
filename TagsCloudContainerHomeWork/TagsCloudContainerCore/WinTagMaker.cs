using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using TagsCloudContainerCore.Console;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore;

[SuppressMessage("Interoperability", "CA1416", MessageId = "Проверка совместимости платформы")]
public class WinTagMaker : ITagMaker<LayoutSettings>
{
    private readonly ILayouter layouter;

    public WinTagMaker(ILayouter layouter)
    {
        this.layouter = layouter;
    }

    public TagToRender MakeTag(KeyValuePair<string, int> raw, LayoutSettings settings, IStatisticMaker statisticMaker)
    {
        var fontSize = GetFontSize(raw, settings.MaxFontSize, statisticMaker);
        var tagSize = GetTagSize(raw.Key, settings.FontName, fontSize, settings.PictureSize);
        var location = layouter.PutNextRectangle(tagSize).Location;
        var color = int.Parse("FF" + settings.FontColor, NumberStyles.HexNumber);

        return new TagToRender(location, raw.Key, color, fontSize, settings.FontName);
    }

    private float GetFontSize(KeyValuePair<string, int> tag, float maxFontSize, IStatisticMaker statMaker)
        => 10 + MathF.Abs(maxFontSize * (tag.Value - statMaker.GetMostFrequentTag().Value) /
                          (statMaker.GetLeastFrequentTag().Value - statMaker.GetMostFrequentTag().Value));

    private Size GetTagSize(string tag, string fontName, float fontSize, Size imageSize)
    {
        if (fontSize == 0)
        {
            return Size.Empty;
        }

        using var mockImage = new Bitmap(imageSize.Width, imageSize.Height);
        using var mockGraphics = Graphics.FromImage(mockImage);
        using var mockFont = new Font(fontName, fontSize);

        return mockGraphics.MeasureString(tag, mockFont).ToSize();
    }
}