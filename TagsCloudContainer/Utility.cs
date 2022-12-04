using System.Drawing;

namespace TagsCloudContainer;

public static class Utility
{
    public static SizeF MeasureString(this string s, Font font)
    {
        var fakeImage = new Bitmap(1, 1);
        var graphics = Graphics.FromImage(fakeImage);
        return graphics.MeasureString(s, font);
    }
}