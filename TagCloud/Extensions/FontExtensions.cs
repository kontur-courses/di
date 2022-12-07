using System.Drawing;

namespace TagCloud.Extensions;

public static class FontExtensions
{
    public static Font ChangeSize(this Font font, float fontSize)
    {
        return new Font(font.Name, fontSize,
            font.Style, font.Unit,
            font.GdiCharSet, font.GdiVerticalFont);
    }
}