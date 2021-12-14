using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using TagsCloudContainerCore.InterfacesCore;

namespace TagsCloudContainerCore;

[SuppressMessage("Interoperability", "CA1416", MessageId = "Проверка совместимости платформы")]
public class WinTagMaker : ITagMaker
{
    public float GetFontSize(KeyValuePair<string, int> tag, IStatisticMaker maker, float maxFontSize)
        => 10 + MathF.Abs(maxFontSize * (tag.Value - maker.GetMinTag().Value) /
                          (maker.GetMaxTag().Value - maker.GetMinTag().Value));

    public Size GetTagSize(string tag, string fontName, float fontSize, Size imageSize)
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