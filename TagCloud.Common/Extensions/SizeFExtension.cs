using System.Drawing;

namespace TagCloud.Common.Extensions;

public static class SizeFExtension
{
    public static Size ConvertToSize(this SizeF sizeF)
    {
        var width = (int)Math.Ceiling(sizeF.Width);
        var height = (int)Math.Ceiling(sizeF.Height);
        return new Size(width, height);
    }
}