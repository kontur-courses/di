using System.Drawing;

namespace TagsCloudContainer;

public static class Utility
{
    private static readonly Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));

    public static SizeF MeasureString(this string s, Font font)
    {
        return graphics.MeasureString(s, font);
    }
}